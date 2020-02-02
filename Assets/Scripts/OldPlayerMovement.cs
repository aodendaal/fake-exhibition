using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(CharacterController))]
public class OldPlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 16f;

    [SerializeField]
    private JoystickController player;
    private string playerInput;

    private GameObject pickupCollision;

    private GameObject exhibitCollision;

    private GameObject carryingObject;
    private Transform carryingPosition;

    private float interactProgress = 0;
    private float interactRate = 0.5f;

    private Slider slider;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = (player == JoystickController.One) ? "Player1 " : "Player2 ";

        carryingPosition = transform.GetChild(1);
        animator = transform.GetChild(0).GetComponent<Animator>();

        slider = GameObject.Find($"{playerInput}Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isTimerStarted)
        {
            MoveAndRotate();
            Interact();
            Drop();
        }

        UpdateInteractSlider();
    }

    private void Drop()
    {
        if (Input.GetButton($"{playerInput}Fire2"))
        {
            if (carryingObject != null)
            {
                var parent = GameObject.Find("Exhibits");
                carryingObject.transform.SetParent(parent.transform);
                carryingObject.transform.position = new Vector3(carryingObject.transform.position.x, 0, carryingObject.transform.position.z);
                carryingObject.transform.GetChild(0).gameObject.SetActive(true);

                carryingObject = null;
            }
        }

    }

    private void UpdateInteractSlider()
    {
        if (interactProgress > 0f)
        {
            slider.gameObject.SetActive(true);
            slider.value = interactProgress;
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }

    private void Interact()
    {
        if (Input.GetButton($"{playerInput}Fire1"))
        {
            if (exhibitCollision != null)
            {
                InteractWithDisplay();
            }
            else if (pickupCollision != null)
            {
                Pickup();
            }
        }
        else
        {
            interactProgress = 0;
        }
    }

    private void Pickup()
    {
        var info = pickupCollision.GetComponent<ExhibitInfo>();

        if (carryingObject == null)
        {
            HoldItem(info);
            info.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    private void InteractWithDisplay()
    {
        var info = exhibitCollision.GetComponent<ExhibitInfo>();

        if (info.exhibitDisplay != null && carryingObject == null)
        {
            var dot = Vector3.Dot(transform.forward, info.facingDirection);

            if (dot > 0.75f)
            {
                if (interactProgress < 1f)
                {
                    interactProgress += Time.deltaTime * interactRate;
                }
                else
                {
                    HoldItem(info);
                    info.exhibitDisplay = null;
                }
            }
            else
            {
                interactProgress = 0f;
            }
        }
    }

    private void HoldItem(ExhibitInfo info)
    {
        carryingObject = info.exhibitDisplay;
        carryingObject.transform.SetParent(carryingPosition);
        carryingObject.transform.localPosition = Vector3.zero;

    }

    private void MoveAndRotate()
    {
        var name = $"{playerInput}Horizontal";
        var h = Input.GetAxis(name);
        var v = Input.GetAxis($"{playerInput}Vertical");
        var move = new Vector3(h, 0, v) * speed;

        if (move != Vector3.zero)
        {
            animator.SetBool("IsCarrying", carryingObject != null);
            animator.SetTrigger("Run");

            var direction = Vector3.RotateTowards(transform.forward, move, Mathf.PI * 2f, 1f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        controller.SimpleMove(move);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 8)
        {
            exhibitCollision = collider.gameObject;
        }
        else if (collider.gameObject.layer == 9)
        {
            pickupCollision = collider.gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        exhibitCollision = null;
    }
}
