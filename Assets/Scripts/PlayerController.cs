﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool movable; //tells if the tank is movable. will be disabled on shot
    private Vector3 direction; //tells the direction of the tank upon left or right arrow press
    public float velocity = 2; //tells the velocity of the tank
    private Rigidbody2D tankBody; //rigidbody of transform. tankBody has a trigger collider so the wheels could do the heavy lifting
    public float jumpForce; //tells how strong the jump of the tank is
    public float jumpTime;
    public bool onJump;
    public bool isGrounded;
    public bool slopeInFront;
    public OrientationChecker orientation;
    
    private Transform wheelFront;

    //control strings
    public string horizontal;
    public string jump;
        
    // Use this for initialization
    void Awake () {
        movable = true;
        tankBody = GetComponent<Rigidbody2D>();
        onJump = false;
        jumpTime = 2.0f;
        orientation = GetComponent<OrientationChecker>();
	}

    void OnDisable()
    {
        tankBody.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    void OnEnable()
    {
        tankBody.constraints = RigidbodyConstraints2D.None;
    }

    void Start()
    {
        wheelFront = transform.GetChild(2).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //first, check if tank is grounded
        isGrounded = Physics2D.Linecast(transform.position, transform.position-1f*Vector3.up, 1<<LayerMask.NameToLayer("Terrain"));
        //Debug.DrawLine(transform.position, transform.position - 1.2f * Vector3.up, Color.red);
        wheelFront.rotation = transform.rotation;

        //then, check if there is a slope in front of the tank
        if (orientation.rightDirection)
        {
            slopeInFront = Physics2D.Linecast(wheelFront.position, wheelFront.position + wheelFront.transform.right * 0.35f - wheelFront.transform.up * 0.35f, 1 << LayerMask.NameToLayer("Terrain"));
            //Debug.DrawLine(wheelFront.position, wheelFront.position + wheelFront.transform.right * 0.35f - wheelFront.transform.up * 0.35f, Color.blue);
        }
        else
        {
            slopeInFront = Physics2D.Linecast(wheelFront.position, wheelFront.position - wheelFront.transform.right * 0.35f - wheelFront.transform.up * 0.35f, 1 << LayerMask.NameToLayer("Terrain"));
            //Debug.DrawLine(wheelFront.position, wheelFront.position - wheelFront.transform.right * 0.35f - wheelFront.transform.up * 0.35f, Color.blue);
        }

        //disable moving when shot is occuring
        if (transform.GetChild(0).GetChild(0).GetComponent<CannonController>().onShot)
        {
            movable = false;
        }
        else
        {
            movable = true;
        }

        //if jump is pressed, tank can move, and is currently not jumping, then a jump must happen
        if (Input.GetButtonDown(jump) && movable && !onJump)
        {
            onJump = true;
            if (orientation.rightDirection)
            {
                tankBody.AddForce(jumpForce * new Vector2(1,1), ForceMode2D.Impulse);
            }
            else
            {
                tankBody.AddForce(jumpForce * new Vector2(-1,1), ForceMode2D.Impulse);
            }
        }

        //if tank is both grounded and on jump, then a tank cannot be on a jump
        if (isGrounded && onJump)
        {
            onJump = false;
        }

        //if movement is pressed, and tank is movable, and is currently a ground, then a move must happen
        if (Input.GetAxis(horizontal) != 0 && movable && isGrounded)
        {
            direction = tankBody.transform.right * Input.GetAxis(horizontal);
            Debug.DrawRay(transform.position, tankBody.transform.right * Input.GetAxis(horizontal), Color.cyan);
            if (slopeInFront)
            {
                tankBody.AddTorque(30f * Input.GetAxis(horizontal));
                direction = direction + 0.4f * transform.up;
            }
            else
            {
                direction = direction - 0.2f * transform.up;
            }

            tankBody.velocity = velocity * direction;
        }
        
        if (Input.GetAxis(horizontal) == 0 && !onJump && tankBody.velocity.magnitude < 0.1f)
        {
            tankBody.velocity = Vector2.zero;
        }
        //if (tankBody.velocity != Vector2.zero)
        //{
        //    //RotateWheels();
        //}
    }

    //rotates wheels during movement
    //void RotateWheels()
    //{
    //    foreach (Transform t in transform)
    //    {
    //        if (t.tag == "Wheels")
    //        {
    //            t.Rotate(new Vector3(0, 0, -tankBody.velocity.magnitude));
    //        }
    //    }
    //}
}
