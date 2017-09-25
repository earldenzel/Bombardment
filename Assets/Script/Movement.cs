using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody2D tankVehicle;
    private RaycastHit2D hit;
    public float velocity = 2.0f;
    private float childPositionX;

	// Use this for initialization
	void Start () {
		tankVehicle = GetComponent<Rigidbody2D>();
        childPositionX = transform.GetChild(0).position.x;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //change orientation of tank in respect to last directional command
            if ((Input.GetAxis("Horizontal") < 0))
            {
                tankVehicle.GetComponent<SpriteRenderer>().flipX = true;
                transform.GetChild(0).eulerAngles = new Vector3(0, 180, 0);
                //transform.GetChild(0).position = new Vector3(-childPositionX, transform.GetChild(0).position.y, 0);
            }
            else
            {
                tankVehicle.GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);
                //transform.GetChild(0).position = new Vector3(childPositionX, transform.GetChild(0).position.y, 0);
            }

            //detects if there is land in front of it       
            hit = Physics2D.Raycast(transform.GetChild(0).position, transform.right, 0.5f);

            //if there is no land in front of tank, apply force normally
            if (hit.collider == null)
            {
                tankVehicle.AddForce(velocity * transform.right * Input.GetAxis("Horizontal"));
            }
            //if there is indeed a piece of land, then try to apply an upward right force
            else if (hit.collider.tag == "Terrain")
            {
                tankVehicle.AddForce(velocity * Vector2.one * Input.GetAxis("Horizontal"));
                tankVehicle.rotation += 1f;
                //tankVehicle.rotation += 0.1f;
            }
        }
        else
        {
            hit = Physics2D.Raycast(new Vector3(transform.position.x,transform.position.y-0.5f), new Vector3(0,-1,0),0.05f);
            //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.33f), new Vector3(0, -1, 0));

            //if vehicle is in a funny angle, the vehicle will right itself up
            if(tankVehicle.rotation > 90 || tankVehicle.rotation <-90)
            {
                transform.up = new Vector3(0, 1, 0);
                tankVehicle.freezeRotation = true;
                tankVehicle.velocity = new Vector3(0, -100, 0);
                transform.rotation = Quaternion.identity;
                tankVehicle.freezeRotation = false;

            }


            //automatically stickies the vehicle to the ground when no movement occurs
            tankVehicle.velocity = 5*-transform.up;

            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.gameObject.name);
            //    tankVehicle.velocity = -transform.up;
            //}
        }
    }
}
