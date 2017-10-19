using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool movable; //tells if the tank is movable. will be disabled on shot
    private Vector3 direction; //tells the direction of the tank upon left or right arrow press
    public float velocity = 2; //tells the velocity of the tank
    private Rigidbody2D tankBody; //rigidbody of transform. tankBody has a trigger collider so the wheels could do the heavy lifting
    private bool rightDirection; //tells if the tank is facing right
    private int collisionCheck; //checks if the tank is touching any terrain

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
        movable = true;
        rightDirection = true;
        tankBody = GetComponent<Rigidbody2D>();
        collisionCheck = 0;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collisionCheck == 0)
        {
            movable = false;
            tankBody.transform.up = Vector2.up;
            tankBody.gravityScale = 1;
        }
        else
        {
            movable = true;
            if (Input.GetAxis("Horizontal") != 0 && Movable)
            {
                tankBody.gravityScale = 1;
                //tankBody.drag = 10;
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
                if (rightDirection)
                {
                    direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0) + tankBody.transform.right - 0.4f * transform.up;
                }
                else
                {
                    direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0) - tankBody.transform.right - 0.4f * transform.up;
                }
                tankBody.velocity = velocity * direction;
            }
            else
            {
                tankBody.gravityScale = 0;
                tankBody.velocity = Vector2.zero;
            }
            if (tankBody.velocity != Vector2.zero)
            {
                RotateWheels();
            }
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

    void OnCollisionStay2D(Collision2D collision)
    {
        tankBody.freezeRotation = false;

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collisionCheck--;
        if (collisionCheck < 2) {
            tankBody.freezeRotation = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCheck++;
        if (collisionCheck >= 2)
        {
            tankBody.freezeRotation = false;
        }
    }
}
