using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour, IInteractive, ICompletable
{
    public float duration;

    private GameObject heldObject;

    public void Interact(GameObject player)
    {
        if (heldObject != null)
            return;

        var inventory = player.GetComponent<PlayerInventory>();

        if (!inventory.IsCarrying)
            return;

        var busy = player.GetComponent<PlayerBusy>();

        if (busy.IsBusy)
            return;

        heldObject = inventory.DropObject();
        busy.ActBusy(gameObject, duration);
    }
    public void Complete(GameObject player)
    {
        var pickup = heldObject.GetComponent<Pickup>();

        GameController.score += pickup.score;
        Destroy(heldObject);
    }

    public void Cancel(GameObject player)
    {
        var inventory = player.GetComponent<PlayerInventory>();

        inventory.HoldObject(heldObject);
        heldObject = null;
    }

}
