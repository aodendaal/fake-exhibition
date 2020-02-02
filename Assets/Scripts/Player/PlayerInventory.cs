using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool IsCarrying => carryingObject != null;
    private GameObject carryingObject;

    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    public void HoldObject(GameObject gameObject)
    {
        playerInfo.animator.SetBool("IsCarrying", true);

        carryingObject = gameObject;
        carryingObject.GetComponent<BoxCollider>().enabled = false;
    }

    public GameObject DropObject()
    {
        carryingObject.GetComponent<BoxCollider>().enabled = true;

        var temp = carryingObject;
        carryingObject = null;

        playerInfo.animator.SetBool("IsCarrying", false);

        return temp;
    }
}
