using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CannonConfig
{
    public float minAngle, maxAngle, rotationSpeed, launchSpeed;
}

public class CannonController : MonoBehaviour {
    public CannonConfig cannon;
    private float zRotate;
    private float parentAngle;
    private bool onShot;
	// Use this for initialization
	void Start () {
        zRotate = 0;
        onShot = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetAxis("Vertical") != 0 && !onShot)
        {
            transform.Rotate(new Vector3(0, 0, Input.GetAxis("Vertical")));

            //this whole block of code respects the base angle of the tank in respect to the angle of the projectile
            zRotate = transform.localRotation.eulerAngles.z;
            if(zRotate > 180)
            {
                zRotate = zRotate - 360;
            }
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            currentRotation.z = Mathf.Clamp(zRotate, cannon.minAngle, cannon.maxAngle);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onShot)
            {
                //here's where I put the code to stop increase of the bar when user presses space again. 
                //bar end strength = shotStrength, then it's not onShot anymore
                Debug.Log("Shot ends");
                onShot = false;
            }
            else
            {
                //here's where I put the code to increase bar.
                Debug.Log("Shot begins");
                onShot = true;
            }
        }
        //here, i will put an if statement that tells me that when the user doesn't hit space after a while, shot ends
        //and bar restarts again

    }
}
