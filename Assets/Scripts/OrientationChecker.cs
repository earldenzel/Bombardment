using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChecker : MonoBehaviour {

    private Vector3 currentRotation;
    private float zRotate;
    
    private PlayerController thisPlayer;

    // Use this for initialization
    void Start()
    {
        thisPlayer = GetComponent<PlayerController>();
    }


    void FixedUpdate ()
    {
        //enables tank to switch direction (without penalty)
        if (Input.GetAxis(thisPlayer.horizontal) != 0 && thisPlayer.enabled)
        {
            //check orientation if facing camera nicely
            if ((Input.GetAxis(thisPlayer.horizontal) > 0))
            {
                if (!thisPlayer.rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(1, 1, 1);
                }
                thisPlayer.rightDirection = true;
            }
            else
            {
                if (thisPlayer.rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                thisPlayer.rightDirection = false;
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
