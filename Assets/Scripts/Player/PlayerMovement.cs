using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerInfo))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private float speed = 16f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInfo = GetComponent<PlayerInfo>();
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis($"{playerInfo.Input}Horizontal");
        var v = Input.GetAxis($"{playerInfo.Input}Vertical");
        var move = new Vector3(h, 0, v) * speed;

        if (move != Vector3.zero)
        {
            playerInfo.animator.SetBool("IsRunning", true);

            transform.rotation = Quaternion.LookRotation(move.normalized);
        }
        else
        {
            playerInfo.animator.SetBool("IsRunning", false);
        }

        controller.SimpleMove(move);
    }
}
