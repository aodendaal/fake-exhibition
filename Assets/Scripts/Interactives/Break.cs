using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Break : MonoBehaviour, IInteractive, ICompletable
{
    public float duration;
    public int score;

    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<PlayerInventory>();

        if (inventory.IsCarrying)
            return;

        var busy = player.GetComponent<PlayerBusy>();

        if (busy.IsBusy)
            return;

        busy.ActBusy(gameObject, duration);
    }

    public void Complete(GameObject player)
    {
        var inventory = player.GetComponent<PlayerInventory>();

        inventory.HoldObject(gameObject);

        var holdItem = player.transform.Find("HoldItem");
        gameObject.transform.SetParent(holdItem);
        gameObject.transform.localPosition = Vector3.zero;

        var pickup = gameObject.AddComponent<Pickup>();
        pickup.score = score;

        Destroy(this);
    }

    public void Cancel(GameObject player)
    {
    }
}
