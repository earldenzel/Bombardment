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
    private bool movable; //checks if vehicle is movable. is triggered by CannonController. if doing a shot, movement is not possible.

    public bool Movable
    {
        get
        {
            return movable;
        }

        set
        {
            movable = value;
        }
    }

    // Use this for initialization
    void Start () {
		tankVehicle = GetComponent<Rigidbody2D>();
        rightDirection = true;
        Movable = true;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetAxis("Horizontal") != 0 && GetComponent<FreefallController>().Controllable && Movable)
        {
            tankVehicle.drag = 10;
            //change orientation of tank in respect to last directional command
            if ((Input.GetAxis("Horizontal") > 0))
            {
                RotateWheels();
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
                RotateWheels();                
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

    //rotates wheels during movement
    void RotateWheels()
    {
        foreach (Transform t in tankVehicle.transform.GetChild(3))
        {
            if (t.GetComponent<SpriteRenderer>().tag == "Wheels")
            {
                t.Rotate(new Vector3(0, 0, -10f));
            }
        }
    }
}