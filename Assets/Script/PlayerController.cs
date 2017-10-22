using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool movable; //tells if the tank is movable. will be disabled on shot
    private Vector3 direction; //tells the direction of the tank upon left or right arrow press
    public float velocity = 2; //tells the velocity of the tank
    private Rigidbody2D tankBody; //rigidbody of transform. tankBody has a trigger collider so the wheels could do the heavy lifting
    private bool rightDirection; //tells if the tank is facing right
    public float jumpForce;
    private float risingAngle;

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

    public bool RightDirection
    {
        get
        {
            return rightDirection;
        }
    }

    // Use this for initialization
    void Start () {
        movable = true;
        rightDirection = true;
        tankBody = GetComponent<Rigidbody2D>();
	}    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && movable)
        {
            if (rightDirection)
            {
                tankBody.AddForce(jumpForce * new Vector2 (1,1), ForceMode2D.Impulse);
            }
            else
            {
                tankBody.AddForce(jumpForce * new Vector2 (-1,1), ForceMode2D.Impulse);
            }
        }
        //if tank movement is not pressed and not on freefall, gravity does not affect the tank
        if (GetComponent<OrientationChecker>().Freefall)
        {
            tankBody.gravityScale = 1;
            //allow some degree of movement on freefall/flight
            if (Input.GetAxis("Horizontal") != 0)
            {
                tankBody.AddForce(Vector2.right * Input.GetAxis("Horizontal") * velocity);
            }
            movable = false;
        }
        else if (Input.GetAxis("Horizontal") == 0 && !GetComponent<OrientationChecker>().Freefall)
        {
            tankBody.gravityScale = 0;
            movable = true;
        }
        else if (Input.GetAxis("Horizontal") != 0 && Movable)
        {
            movable = true;
            tankBody.gravityScale = 1;
            if ((Input.GetAxis("Horizontal") > 0))
            {
                if (!rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rightDirection = true;
            }
            else
            {
                if (rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rightDirection = false;
            }

            //set direction for tank depending on where it's facing
            //if (rightDirection)
            //{
            //    direction = tankBody.transform.right * Input.GetAxis("Horizontal") - 0.2f*transform.up;
            //}
            //else
            //{
            //    direction = tankBody.transform.right * Input.GetAxis("Horizontal") - 0.2f*transform.up;
            //}
            direction = tankBody.transform.right * Input.GetAxis("Horizontal") -0.2f*transform.up;
            risingAngle = Vector3.Angle(transform.right, Vector3.right);
            if (risingAngle > 70.0f)
            {
                tankBody.velocity = Vector2.zero;
            }
            else
            {
                tankBody.velocity = velocity * direction;
            }
            
        }
        if (tankBody.velocity != Vector2.zero)
        {
            RotateWheels();
        }
    }

    //rotates wheels during movement
    void RotateWheels()
    {
        foreach (Transform t in transform)
        {
            if (t.GetComponent<SpriteRenderer>().tag == "Wheels")
            {
                t.Rotate(new Vector3(0, 0, -tankBody.velocity.magnitude));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        tankBody.gravityScale = 0;
        tankBody.velocity = Vector2.zero;
    }
}
