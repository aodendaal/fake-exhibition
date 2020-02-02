using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]
public class PlayerInteract : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton($"{playerInfo.Input}Fire1"))
        {
            var ray = new Ray(transform.position + (Vector3.up / 2f), transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 2f, 1 << 8))
            {
                var interactive = hitInfo.collider.gameObject.GetComponent<IInteractive>();

                interactive.Interact(gameObject);
            }
        }
    }
}
