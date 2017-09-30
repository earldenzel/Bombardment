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

	// Use this for initialization
	void Start () {
		tankVehicle = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetAxis("Horizontal") != 0 && GetComponent<FreefallController>().Controllable)
        //if (Input.GetAxis("Horizontal") != 0)
        {
            //change orientation of tank in respect to last directional command
            if ((Input.GetAxis("Horizontal") > 0))
            {
                rightDirection = true;
                tankVehicle.GetComponent<SpriteRenderer>().flipX = false;
                facingPosition = transform.GetChild(0).position;
                hit = Physics2D.Raycast(facingPosition, transform.right, 0.5f);
            }
            else
            {
                rightDirection = false;
                tankVehicle.GetComponent<SpriteRenderer>().flipX = true;
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
            tankVehicle.velocity = Vector3.zero; //if there is no directional input, set velocity to 0
        }
    }
}