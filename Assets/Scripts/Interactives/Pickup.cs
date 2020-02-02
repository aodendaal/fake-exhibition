using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteractive
{
    public int score;

    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<PlayerInventory>();

        if (inventory.IsCarrying)
            return;

        var busy = player.GetComponent<PlayerBusy>();

        if (busy.IsBusy)
            return;

        inventory.HoldObject(gameObject);

        var holdItem = player.transform.Find("HoldItem");
        gameObject.transform.SetParent(holdItem);
        gameObject.transform.localPosition = Vector3.zero;
    }
}
