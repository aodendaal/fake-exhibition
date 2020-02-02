using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private float showTime;
    private float showRate = 0.1f;

    private GameObject hintObject;
    private bool showingHint;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Hint")
            {
                hintObject = child.gameObject;
                break;
            }
        }

        hintObject.SetActive(false);
    }

    private void Update()
    {
        LookAtCamera();

        if (showingHint)
        {
            if (Time.time > showTime)
            {
                hintObject.SetActive(false);
                showingHint = false;
            }
        }
    }

    private void LookAtCamera()
    {
        var direction = Camera.main.transform.position - transform.position;

        hintObject.transform.rotation = Quaternion.LookRotation(-direction.normalized);

    }

    public void ShowHint()
    {
        hintObject.SetActive(true);
        showingHint = true;

        showTime = Time.time + showRate;
    }
}
