using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTextAnimation : MonoBehaviour
{
    private bool rising;
    private float risingStartTime;
    
    private void OnEnable()
    {
        rising = true;
        risingStartTime = Time.time;
    }

    private void Update()
    {
        if (rising && (Time.time - risingStartTime) < .7f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 20);
        }
        else
        {
            rising = false;
            gameObject.SetActive(false);
        }
    }
}
