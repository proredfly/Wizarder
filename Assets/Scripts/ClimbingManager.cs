using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClimbingManager : MonoBehaviour
{
    public GameObject[] holds;
    public int currentHold;
    CharacterController cc;
    FirstPersonController fpc;
    public CapsuleCollider capsule;
    public float speed;
    Vector3 desiredPosition;
    bool climbing = false;
    
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        fpc = GetComponent<FirstPersonController>();
    }

    private void FixedUpdate()
    {
        //make sure position is the "desired position"
        if (climbing)
        {
            MoveTowardsTarget(desiredPosition);
        }
    }
    

    //move towards the target position
    public void MoveTowardsTarget(Vector3 dp)
    {
        Vector3 offset = dp - cc.transform.position;
        if (offset.magnitude > 0.1f)
        {
            offset = offset.normalized * speed;
            cc.Move(offset * Time.deltaTime);
        }

        
    }

    //start climbing and set necessary variables and call necessary methods
    public void InitiateClimbing(int h)
    {
        climbing = true;
        Physics.IgnoreLayerCollision(8, 10);
        capsule.enabled = false;
        fpc.climbing = true;
        currentHold = h;
        GoToHold(h);

    }

    //self explanatory
    public void NewHold(int h)
    {
        currentHold = h;
        GoToHold(h);
    }

    //changes variables!
    public void ExitClimbing()
    {
        if (climbing)
        {
            capsule.enabled = true;
            Physics.IgnoreLayerCollision(8, 10, false);
            climbing = false;
            fpc.climbing = false;
        }
    }
       
    //sets the desired position
    public void GoToHold(int h)
    {
        desiredPosition = holds[h].transform.GetChild(0).transform.position;
        desiredPosition.y -= 1.375f;
    }
}
