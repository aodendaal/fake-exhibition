using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHint : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(transform.position + (Vector3.up / 2f), transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 2f, 1 << 8))
        {
            var go = hitInfo.collider.gameObject;

            var hint = go.GetComponent<Hint>();

            if (hint != null)
            {
                hint.ShowHint();
            }
        }
    }
}
