using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Hold : MonoBehaviour
{
    public Renderer r;
    public float speed;
    public int holdNum;
    public Color startColor, endColor;
    public float startTime;
    public GameObject _mainCamera;
    Boolean inProgress = false;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    public void FixedUpdate()
    {
        
        // enable particle system on hold if close enough
        if(Vector3.Distance(transform.position, _mainCamera.transform.position) <= 3)
        {
            var eTemp = GetComponentInChildren<ParticleSystem>().emission;
            eTemp.enabled = true;
        } else
        {
            var eTemp = GetComponentInChildren<ParticleSystem>().emission;
            eTemp.enabled = false;
            GetComponentInChildren<ParticleSystem>().Clear();
        }
       
        // when end color is different it runs changeColor method
        // in progress to make sure it doesnt run when 
        if (startColor != endColor && !inProgress)
        {
            inProgress = true;
            // create coroutine to change color with fixed update
            StartCoroutine(ChangeColor());
            startTime = Time.time;
        } 
    }

    // change color over time 
    // generally ran as coroutine with fixed update
    public IEnumerator ChangeColor()
    {
        float tick = 0f;
        while (r.material.color != endColor)
        {
            tick += Time.deltaTime * speed;
            r.material.color = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }

        //reset and go back to white with the same method
        Color temp = endColor;
        endColor = startColor;
        startColor = temp;
        tick = 0f;
        startTime = Time.time;
        while (r.material.color != endColor)
        {
            tick += Time.deltaTime * speed;
            r.material.color = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }
        startColor = endColor;
        inProgress = false;
    }
}
