using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMover : MonoBehaviour
{
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(playerTransform.position);
        var rect = transform as RectTransform;
        rect.position = pos;
    }
}
