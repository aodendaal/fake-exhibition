using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Audience : MonoBehaviour
{
    private float playTime;

    void Start()
    {
        playTime = Time.time + Random.Range(0f, 4f);
    }

    void Update()
    {
        if (playTime < Time.time)
        {
            var seq = LeanTween.sequence();
            seq.append(LeanTween.moveLocalY(gameObject, 0.5f, 0.1f));
            seq.append(LeanTween.moveLocalY(gameObject, 0f, 0.1f).setEaseInBounce());
            seq.append(() => playTime = Time.time + Random.Range(0f, 4f));
        }
    }
}
