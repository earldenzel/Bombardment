using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CannonConfig
{
    public float minAngle;
    public float maxAngle;
    public GameObject shot1;
    public GameObject shot2;
    public float launchSpeed;
}

public class CannonController : MonoBehaviour {
    public CannonConfig cannon;
    private float zRotate;
    private float parentAngle;
    private bool onShot; //tells if the player is currently taking a shot.
    private bool shot1; // tells if the current shot is shot1. if shot1 = false, then shot2
    //private bool yourTurn; //tells if it's the current player's turn. for now, it will be set to true.
    private GameObject currentProjectile;
    private Quaternion spawnRotation;

	// Use this for initialization
	void Start () {
        onShot = false;
        shot1 = true;
        //yourTurn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //SetShotToShot1(!shot1);
            shot1 = !shot1;
        }
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
                ShootProjectile();
                transform.root.GetComponent<Movement>().Movable = true;
                onShot = false;
            }
            else
            {
                //here's where I put the code to increase bar.
                Debug.Log("Shot begins");
                LoadShotOne(shot1);
                onShot = true;
            }
        }
        //here, i will put an if statement that tells me that when the user doesn't hit space after a while, shot ends
        //and bar restarts again

    }

    private void ShootProjectile()
    {
        Vector2 forceVec = transform.GetChild(0).position - this.transform.position;
        forceVec *= cannon.launchSpeed;
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
        currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void LoadShotOne(bool shot1)
    {
        //tank does not move when loading shot
        transform.root.GetComponent<Movement>().Movable = false;
        //whole block checks orientation first of the tank before attempting to create projectile object
        if (transform.root.transform.localScale.x > 0)
        {
            spawnRotation = transform.rotation;
        }
        else
        {
            spawnRotation = Quaternion.Euler(0, 0, 180f) * transform.rotation;
        }
        //spawns projectile       
        if (shot1)
        {
            currentProjectile = Instantiate(cannon.shot1, transform.GetChild(0).position, spawnRotation) as GameObject;
        }
        else
        {
            currentProjectile = Instantiate(cannon.shot2, transform.GetChild(0).position, spawnRotation) as GameObject;
        }

        currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
