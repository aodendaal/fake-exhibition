using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]
public class PlayerDrop : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    void Update()
    {
        if (!GameController.isStarted || GameController.isPaused)
            return;

        if (Input.GetButton($"{playerInfo.Input}Fire2"))
        {
            var inventory = gameObject.GetComponent<PlayerInventory>();

            if (inventory.IsCarrying)
            {
                var go = inventory.DropObject();

                go.transform.SetParent(null);
                go.transform.position = new Vector3(go.transform.position.x, 0f, go.transform.position.z);
            }
        }
    }
}
