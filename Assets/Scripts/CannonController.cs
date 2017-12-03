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
    private SpriteRenderer powerBar;
    public AnimationCurve powerCurve;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private float myTime;
    private CameraController cameraController;

    public bool OnShot
    {
        get
        {
            return onShot;
        }

        set
        {
            onShot = value;
        }
    }

    // Use this for initialization
    void Start () {
        OnShot = false;
        shot1 = true;
        //yourTurn = true;
        powerBar = transform.GetChild(1).GetComponent<SpriteRenderer>();
        originalScale = powerBar.transform.localScale;
        originalPosition = powerBar.transform.localPosition;
        powerBar.enabled = false;

        if (Camera.main.GetComponent<CameraController>() != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (onShot)
        {
            myTime += Time.deltaTime/2;
            powerBar.transform.localScale = new Vector3(3f*powerCurve.Evaluate(myTime), originalScale.y, originalScale.z);
            powerBar.transform.localPosition = new Vector3(0.75f*powerCurve.Evaluate(myTime), 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !OnShot)
        {
            shot1 = !shot1;
        }
        if (Input.GetAxis("Vertical") != 0 && !OnShot)
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
            transform.root.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (OnShot)
            {
                //shot ends
                ShootProjectile();
                transform.root.GetComponent<PlayerController>().Movable = true;
                StartCoroutine(ShowLastPower());
                OnShot = false;
            }
            else
            {
                //shot begins
                myTime = 0.0f;
                LoadShotOne(shot1);
                OnShot = true;
            }
        }
    }

    private IEnumerator ShowLastPower()
    {
        yield return new WaitForSeconds(1.0f);
        powerBar.enabled = false;
    }

    private void ShootProjectile()
    {
        Vector2 forceVec = (transform.GetChild(0).position - this.transform.position).normalized;
        forceVec *= (cannon.launchSpeed * powerBar.transform.localScale.x);
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
        currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;

        if(currentProjectile.tag == "Arrow")
        {
            StartCoroutine(TwoMoreShots(forceVec));
        }

        //Change the target state for the camera
        if (cameraController != null)
        {
            cameraController.ObjectTracer.SetFoucs(currentProjectile);
        }
    }

    private IEnumerator TwoMoreShots(Vector2 shotStrength)
    {
        yield return new WaitForSeconds(0.3f);
        GameObject secondShot = Instantiate(cannon.shot2, transform.GetChild(0).position, spawnRotation) as GameObject;
        secondShot.GetComponent<ProjectileController>().cannon = this;
        secondShot.GetComponent<Rigidbody2D>().AddForce(shotStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        GameObject thirdShot = Instantiate(cannon.shot2, transform.GetChild(0).position, spawnRotation) as GameObject;
        thirdShot.GetComponent<ProjectileController>().cannon = this;
        thirdShot.GetComponent<Rigidbody2D>().AddForce(shotStrength, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        OnShot = false;
    }

    private void LoadShotOne(bool shot1)
    {
        //spawn powerbar gauge
        powerBar.transform.localScale = originalScale;
        powerBar.transform.localPosition = originalPosition;
        powerBar.enabled = true;
        //tank does not move when loading shot
        transform.root.GetComponent<PlayerController>().Movable = false;
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
        currentProjectile.GetComponent<ProjectileController>().cannon = this; 
    }
}
