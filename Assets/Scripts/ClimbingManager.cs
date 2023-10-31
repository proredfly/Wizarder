using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbingManager : MonoBehaviour
{
    [SerializeField] public GameObject[] holds;
    public int currentHold;
    CharacterController cc;
    FirstPersonController fpc;
    Vector3 desiredPosition;
    bool climbing = false;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        fpc = GetComponent<FirstPersonController>();
    }

    private void FixedUpdate()
    {
        if(Math.Abs(Vector3.Distance(desiredPosition, cc.transform.position)) >= 0 && climbing)
        {
            cc.transform.position = desiredPosition;
        }
    }
    public void InitiateClimbing(int h)
    {
        climbing = true;
        fpc.isClimbing = true;
        currentHold = h;
        GoToHold(h);

    }

    public void NewHold(int h)
    {
        currentHold = h;
        GoToHold(h);
    }

    public void ExitClimbing()
    {
        if (climbing)
        {
            climbing = false;
            fpc.isClimbing = false;
        }
    }
       
    public void GoToHold(int h)
    {
        desiredPosition = holds[h].transform.GetChild(0).transform.position;
        desiredPosition.y -= 1.375f;
    }
}
