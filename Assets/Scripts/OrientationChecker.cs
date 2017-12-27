using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChecker : MonoBehaviour {

    private Vector3 currentRotation;
    private float zRotate;
    public bool rightDirection; //tells if the tank is facing right

    private PlayerController thisPlayer;
    private string horizontal;
    public bool onTurn;
    public bool isGrounded;

    // Use this for initialization
    void Start()
    {
        thisPlayer = GetComponent<PlayerController>();
        rightDirection = true;
        horizontal = thisPlayer.horizontal;
    }
    
    void FixedUpdate ()
    {
        //first, check if tank is grounded
        isGrounded = Physics2D.Linecast(transform.position, transform.position - 1.5f * Vector3.up, 1 << LayerMask.NameToLayer("Terrain"));        
        
        //fix orientation
        zRotate = transform.rotation.eulerAngles.z;
        if (zRotate > 180)
        {
            zRotate = zRotate - 360;
        }
        
        if (zRotate > 50f)
        {
            transform.Rotate(0, 0, 50f - zRotate);
        }
        else if (zRotate < -50f)
        {
            transform.Rotate(0, 0, -50f - zRotate);
        }

        //enables tank to switch direction (without penalty)
        if (Input.GetAxis(horizontal) != 0 && onTurn)
        {
            //check orientation if facing camera nicely
            if ((Input.GetAxis(horizontal) > 0))
            {
                if (!rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rightDirection = true;
            }
            else
            {
                if (rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rightDirection = false;
            }
        }
    }
}
