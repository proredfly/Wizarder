using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Renderer r;
    public float speed;
    public int holdNum;
    public Color startColor, endColor;
    public float startTime;
    Boolean inProgress = false;

    public void FixedUpdate()
    {
        if(startColor != endColor && !inProgress)
        {
            inProgress = true;
            StartCoroutine(ChangeColor());
            startTime = Time.time;
        } 
    }
    public IEnumerator ChangeColor()
    {
        float tick = 0f;
        while (r.material.color != endColor)
        {
            tick += Time.deltaTime * speed;
            r.material.color = Color.Lerp(startColor, endColor, tick);
            Debug.Log("attempting to work");
            yield return null;
        }
        Color temp = endColor;
        endColor = startColor;
        startColor = temp;
        tick = 0f;
        startTime = Time.time;
        while (r.material.color != endColor)
        {
            tick += Time.deltaTime * speed;
            r.material.color = Color.Lerp(startColor, endColor, tick);
            Debug.Log("attempting to work");
            yield return null;
        }
        startColor = endColor;
        inProgress = false;
    }
}
