using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChecker : MonoBehaviour {

    private Vector3 currentRotation;
    private float zRotate;
    	
	void FixedUpdate ()
    {
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
