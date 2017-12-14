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

    // Use this for initialization
    void Start()
    {
        thisPlayer = GetComponent<PlayerController>();
        rightDirection = true;
        horizontal = thisPlayer.horizontal;
    }
    
    void FixedUpdate ()
    {
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

        //fix orientation
        zRotate = transform.rotation.eulerAngles.z;
        if (zRotate > 180)
        {
            zRotate = zRotate - 360;
        }
        currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = Mathf.Clamp(zRotate, -60f, 60f);
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
