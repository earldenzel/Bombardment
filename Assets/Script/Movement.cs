using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody2D tankVehicle;
    private RaycastHit2D hit;
    public float velocity = 2.0f;
    private bool rightDirection;
    private Vector3 direction;
    private Vector3 facingPosition;
    public float force = 10.0f;
    private bool onFlight;

    public bool OnFlight
    {
        get
        {
            return onFlight;
        }
        set
        {
            onFlight = value;
        }

    }

    // Use this for initialization
    void Start () {
		tankVehicle = GetComponent<Rigidbody2D>();
        rightDirection = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetAxis("Horizontal") != 0 && GetComponent<FreefallController>().Controllable)
        {
            tankVehicle.drag = 10;
            //change orientation of tank in respect to last directional command
            if ((Input.GetAxis("Horizontal") > 0))
            {
                if (!rightDirection)
                {
                    tankVehicle.transform.localScale = new Vector3(1, 1, 1);
                }
                rightDirection = true;

                facingPosition = transform.GetChild(0).position;
                hit = Physics2D.Raycast(facingPosition, transform.right, 0.5f);
            }
            else
            {
                if (rightDirection)
                {
                    tankVehicle.transform.localScale = new Vector3(-1, 1, 1);
                }
                rightDirection = false;

                facingPosition = transform.GetChild(1).position;
                hit = Physics2D.Raycast(facingPosition, -transform.right, 0.5f);
            }

            //set direction for tank depending on where it's facing
            if (rightDirection)
            {
                direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0) + transform.right - 0.5f * transform.up;
            }
            else
            {
                direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0) - transform.right - 0.5f * transform.up;
            }

            //if there is a terrain collider in front of the tank, try to change direction upwards
            if (hit.collider != null && hit.collider.tag == "Terrain")
            {
                direction = direction + transform.up;
            }
            //move depending on calculated direction
            tankVehicle.velocity = velocity * direction;
        }
        else
        {
            tankVehicle.velocity = Vector2.zero;
            tankVehicle.drag = 1000000;
            //if there is no directional input, set velocity to 0
        }
    }
}