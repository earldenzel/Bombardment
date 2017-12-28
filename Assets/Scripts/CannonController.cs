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
    private GameObject currentProjectile;
    private Quaternion spawnRotation;
    private SpriteRenderer powerBar;
    public AnimationCurve powerCurve;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private float myTime;
    private CameraController cameraController;
    public bool fired;
    public bool canLoadStrongerShot;

    public string vertical;
    public string shoot;
    public string switchShot;

    private CanvasController UICanvas;
    private LineRenderer trajectory;
    
    // Use this for initialization
    void Start () {
        onShot = false;
        trajectory = GetComponent<LineRenderer>();
        powerBar = transform.GetChild(1).GetComponent<SpriteRenderer>();
        originalScale = powerBar.transform.localScale;
        originalPosition = powerBar.transform.localPosition;
        powerBar.transform.localScale = new Vector3(0, originalScale.y, originalScale.z);
        powerBar.enabled = false;
        fired = false;
        UICanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();

        if (Camera.main.GetComponent<CameraController>() != null)
        {
            cameraController = Camera.main.GetComponent<CameraController>();
        }
        LoadWeakerShot();        
    }

    // Update is called once per frame
    void Update ()
    {
        UICanvas.UpdatePower(powerBar.transform.localScale.x/3);
        if (onShot)
        {
            myTime += Time.deltaTime / 2;
            if (transform.parent.tag == "Scorch" || transform.parent.tag == "Scorch2")
            {
                powerBar.transform.localScale = new Vector3(3f * powerCurve.Evaluate(myTime), originalScale.y, originalScale.z);
                powerBar.transform.localPosition = new Vector3(0, 0.75f*powerCurve.Evaluate(myTime), 0);
            }
            else
            {
                powerBar.transform.localScale = new Vector3(3f * powerCurve.Evaluate(myTime), originalScale.y, originalScale.z);
                powerBar.transform.localPosition = new Vector3(0.75f * powerCurve.Evaluate(myTime), 0, 0);
            }
            RenderTrajectory(powerBar.transform.localScale.x * cannon.launchSpeed);
        }
        if (Input.GetButtonDown(switchShot) && !onShot && canLoadStrongerShot && shot1)
        {
            LoadStrongerShot();
        }
        else if (Input.GetButtonDown(switchShot) && !onShot && !shot1)
        {
            LoadWeakerShot();
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
                onShot = false;
                fired = true;
                StartCoroutine(ShowLastPower());
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
    private void RenderTrajectory(float strength)
    {
        trajectory.enabled = true;
        Vector2 initialDirection = (transform.GetChild(0).position - this.transform.position).normalized;
        Vector2 initialPosition = this.transform.position;
        Vector2 initialVelocity = initialDirection * strength;
        Vector3[] positions = new Vector3[100];
        float t = 0;
        for (int i = 0; i <= 99; i++)
        {
            t += Time.deltaTime;

            float dx = initialVelocity.x * t;
            float dy = initialVelocity.y * t - (0.5f * (Physics2D.gravity.magnitude) * (t * t));
            positions[i] = new Vector3(initialPosition.x + dx, initialPosition.y + dy, 1);
        }
        trajectory.SetPositions(positions);
    }

    private void LoadStrongerShot()
    {
        if (!canLoadStrongerShot)
        {
            GameManager.Instance.StageController.MakeAnnouncement("Insufficient amount of fuel to load stronger shot!", 2);
        }
        shot1 = false;
        UICanvas.UpdateUI(transform.root.gameObject);
    }

    public void LoadWeakerShot()
    {
        shot1 = true;
        UICanvas.UpdateUI(transform.root.gameObject);
    }

    public void InstantiateShot()
    {
        fired = false;
    }

    private IEnumerator ShowLastPower()
    {
        yield return new WaitForSeconds(1.0f);
        powerBar.enabled = false;
        trajectory.enabled = false;
    }

    private void ShootProjectile()
    {
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
        SetProjectileProperties(currentProjectile);

        if (currentProjectile != null)
        {
            //determine direction of shooting
            Vector2 forceVec = (transform.GetChild(0).position - this.transform.position).normalized;
            //scorch's projectiles becomes inverted when facing the other way. this fixes it
            if (currentProjectile.tag == "Scorch" || currentProjectile.tag == "Scorch2")
            {
                currentProjectile.transform.localScale = new Vector3(1, 1, 1);
            }
            //throw projectile
            forceVec *= (cannon.launchSpeed * powerBar.transform.localScale.x);
            currentProjectile.GetComponent<Rigidbody2D>().AddForce(forceVec, ForceMode2D.Impulse);
            currentProjectile.GetComponent<Rigidbody2D>().gravityScale = 1;
            //disable current tank
            transform.root.GetComponent<PlayerController>().enabled = false;
            //this only applies to archer's and trebuchet's shots
            if (currentProjectile.tag == "Arrow")
            {
                StartCoroutine(MoreShots(forceVec, cannon.shot2, 2, 0.3f));
            }
            else if (currentProjectile.tag == "Trebuchet")
            {
                StartCoroutine(MoreShots(forceVec, cannon.shot1, UnityEngine.Random.Range(1,3), 0.2f));
            }
            //Change the target state for the camera
            if (cameraController != null)
            {
                cameraController.ObjectTracer.SetFoucs(currentProjectile);
            }
        }
        else
        {
            onShot = false;
        }
    }

    private IEnumerator MoreShots(Vector2 shotStrength, GameObject currentShot, int shotCount, float waitTime)
    {
        for (int i = 1; i <= shotCount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject shot;
            shot = Instantiate(currentShot, transform.GetChild(0).position, spawnRotation) as GameObject;
            SetProjectileProperties(shot);
            shot.GetComponent<Rigidbody2D>().AddForce(shotStrength, ForceMode2D.Impulse);
        }
    }

    private void LoadShotOne(bool shot1)
    {
        //spawn powerbar gauge
        powerBar.transform.localScale = originalScale;
        powerBar.transform.localPosition = originalPosition;
        powerBar.enabled = true;
        //tank does not move when loading shot
        transform.root.GetComponent<PlayerController>().movable = false;
    }

    private void SetProjectileProperties(GameObject projectile)
    {
        //set projectile cannon and attacker
        projectile.GetComponent<ProjectileController>().cannon = this;
        projectile.GetComponent<ProjectileController>().attacker = transform.root.gameObject;
        //maintains fidelity of projectile even when facing left
        if (!transform.root.GetComponent<OrientationChecker>().rightDirection)
        {
            projectile.transform.localScale = new Vector3(1, -1, 1);
        }
        GameManager.Instance.GameData.TotalProjectile += 1;
    }
}
