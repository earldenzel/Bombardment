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
    public bool onShot; //tells if the player is currently taking a shot.
    public bool shot1; // tells if the current shot is shot1. if shot1 = false, then shot2
    //private bool yourTurn; //tells if it's the current player's turn. for now, it will be set to true.
    private GameObject currentProjectile;
    private Quaternion spawnRotation;
    private SpriteRenderer powerBar;
    public AnimationCurve powerCurve;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private float myTime;
    private CameraController cameraController;
    public bool fired;

    public string vertical;
    public string shoot;
    public string switchShot;
    
    // Use this for initialization
    void Start () {
        onShot = false;
        shot1 = true;
        powerBar = transform.GetChild(1).GetComponent<SpriteRenderer>();
        originalScale = powerBar.transform.localScale;
        originalPosition = powerBar.transform.localPosition;
        powerBar.transform.localScale = new Vector3(0, originalScale.y, originalScale.z);
        powerBar.enabled = false;
        fired = false;

        if (Camera.main.GetComponent<CameraController>() != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().UpdatePower(powerBar.transform.localScale.x);
        if (onShot)
        {
            myTime += Time.deltaTime / 2;
            if (transform.parent.tag == "Scorch")
            {
                powerBar.transform.localScale = new Vector3(3f * powerCurve.Evaluate(myTime), originalScale.y, originalScale.z);
                powerBar.transform.localPosition = new Vector3(0, 0.75f*powerCurve.Evaluate(myTime), 0);
            }
            else
            {
                powerBar.transform.localScale = new Vector3(3f * powerCurve.Evaluate(myTime), originalScale.y, originalScale.z);
                powerBar.transform.localPosition = new Vector3(0.75f * powerCurve.Evaluate(myTime), 0, 0);
            }
        }
        if (Input.GetButtonDown(switchShot) && !onShot)
        {
            shot1 = !shot1;
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().UpdateUI(transform.root.gameObject);
        }
        if (Input.GetAxis(vertical) != 0 && !onShot)
        {
            transform.Rotate(new Vector3(0, 0, Input.GetAxis(vertical)));

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
        if (Input.GetButtonDown(shoot))
        {
            transform.root.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (onShot)
            {
                //shot ends and cannot be shot anymore
                ShootProjectile();
                transform.root.GetComponent<PlayerController>().movable = true;
                StartCoroutine(ShowLastPower());
                onShot = false;
                fired = true;                
            }
            else
            {
                //shot begins if shot hasn't been fired
                if (!fired)
                {
                    myTime = 0.0f;
                    LoadShotOne(shot1);
                    onShot = true;
                }
            }
        }
    }

    public void InstantiateShot()
    {
        fired = false;
    }

    private IEnumerator ShowLastPower()
    {
        yield return new WaitForSeconds(1.0f);
        powerBar.enabled = false;
    }

    private void ShootProjectile()
    {
        //determine direction of shooting
        Vector2 forceVec = (transform.GetChild(0).position - this.transform.position).normalized;
        //scorch's projectiles becomes inverted when facing the other way. this fixes it
        if (currentProjectile.tag == "Scorch")
        {
            currentProjectile.transform.localScale = new Vector3(1, 1, 1);
        }
        //throw projectile
        forceVec *= (cannon.launchSpeed * powerBar.transform.localScale.x);
        currentProjectile.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
        currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;
        //disable current tank
        transform.root.GetComponent<PlayerController>().enabled = false;
        //this only applies to archer's shots
        if(currentProjectile.tag == "Arrow")
        {
            StartCoroutine(TwoMoreShots(forceVec));
        }
        //Change the target state for the camera
        if (cameraController != null)
        {
            cameraController.ObjectTracer.SetFoucs(currentProjectile);
        }

        StartCoroutine(NextPlayer());
    }

    private IEnumerator NextPlayer()
    {
        yield return new WaitForSeconds(7.0f);
        GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
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
        onShot = false;
    }

    private void LoadShotOne(bool shot1)
    {
        //spawn powerbar gauge
        powerBar.transform.localScale = originalScale;
        powerBar.transform.localPosition = originalPosition;
        powerBar.enabled = true;
        //tank does not move when loading shot
        transform.root.GetComponent<PlayerController>().movable = false;
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
        //maintains fidelity of projectile even when facing left
        if (!transform.root.GetComponent<PlayerController>().rightDirection)
        {
            currentProjectile.transform.localScale = new Vector3(1, -1, 1);
        }
        currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 0;
        currentProjectile.GetComponent<ProjectileController>().cannon = this;
        currentProjectile.GetComponent<ProjectileController>().attacker = transform.root.gameObject;
    }
}
