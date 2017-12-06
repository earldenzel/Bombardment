using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool movable; //tells if the tank is movable. will be disabled on shot
    private Vector3 direction; //tells the direction of the tank upon left or right arrow press
    public float velocity = 2; //tells the velocity of the tank
    private Rigidbody2D tankBody; //rigidbody of transform. tankBody has a trigger collider so the wheels could do the heavy lifting
    public bool rightDirection; //tells if the tank is facing right
    public float jumpForce;
    private float risingAngle;

    public string horizontal;
    public string jump;
        
    // Use this for initialization
    void Start () {
        movable = true;
        rightDirection = true;
        tankBody = GetComponent<Rigidbody2D>();
	}    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown(jump) && movable)
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
        if (GetComponent<OrientationChecker>().freefall)
        {
            tankBody.gravityScale = 1;
            //allow some degree of movement on freefall/flight
            if (Input.GetAxis(horizontal) != 0)
            {
                tankBody.AddForce(Vector2.right * Input.GetAxis(horizontal) * velocity);
            }
            movable = false;
        }
        else if (Input.GetAxis(horizontal) == 0 && !GetComponent<OrientationChecker>().freefall)
        {
            tankBody.gravityScale = 0;
            if (transform.GetChild(0).GetChild(0).GetComponent<CannonController>().onShot)
            {
                movable = false;
            }
            else
            {
                movable = true;
            }
        }
        else if (Input.GetAxis(horizontal) != 0 && movable)
        {
            movable = true;
            tankBody.gravityScale = 1;
            if ((Input.GetAxis(horizontal) > 0))
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
                VelocityZero();
                if (rightDirection)
                {
                    //flip
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                rightDirection = false;
            }

            direction = tankBody.transform.right * Input.GetAxis(horizontal) -0.2f*transform.up;
            risingAngle = Vector3.Angle(transform.right, Vector3.right);
            if (risingAngle > 70.0f)
            {
                VelocityZero();
                GetComponent<OrientationChecker>().freefall = true;
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
            if (t.tag == "Wheels")
            {
                t.Rotate(new Vector3(0, 0, -tankBody.velocity.magnitude));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        tankBody.gravityScale = 0;
        VelocityZero();
    }

    void VelocityZero()
    {
        tankBody.velocity = Vector2.zero;
        tankBody.angularVelocity = 0;
    }
}
