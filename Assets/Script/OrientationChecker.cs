using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChecker : MonoBehaviour {

    private Transform wheel1;
    private Transform wheel2;
    private RaycastHit2D hit1;
    private RaycastHit2D hit2;
    private Rigidbody2D tankBody;
    private string message;
    private bool freefall;

    public bool Freefall
    {
        get
        {
            return freefall;
        }

        set
        {
            freefall = value;
        }
    }

    // Use this for initialization
    void Start () {
        wheel1 = transform.GetChild(1).transform;
        wheel2 = transform.GetChild(2).transform;
        tankBody = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        hit1 = Physics2D.Raycast(new Vector3(wheel1.position.x, wheel1.position.y - 0.38f), Vector3.down, 0.5f);
        hit2 = Physics2D.Raycast(new Vector3(wheel2.position.x, wheel2.position.y - 0.38f), Vector3.down, 0.5f);
        if (hit1.collider == GetComponent<Collider2D>() || hit2.collider == GetComponent<Collider2D>())
        {
            tankBody.transform.up = Vector3.up;
            freefall = true;
        }
        else if (hit1.collider == null && hit2.collider == null)
        {
            freefall = true;
            hit1 = Physics2D.Raycast(new Vector3(wheel1.position.x, wheel1.position.y - 0.38f), Vector3.down);
            hit2 = Physics2D.Raycast(new Vector3(wheel2.position.x, wheel2.position.y - 0.38f), Vector3.down);
            tankBody.transform.right = (hit2.point - hit1.point) * ((GetComponent<PlayerController>().RightDirection) ? 1 : -1);
        }
        else
        {
            freefall = false;
            //if (hit1.collider == null)
            //{
            //    tankBody.AddForceAtPosition(Vector2.down, wheel1.position, ForceMode2D.Impulse);
            //}
            //else if (hit2.collider == null)
            //{
            //    tankBody.AddForceAtPosition(Vector2.down, wheel2.position, ForceMode2D.Impulse);
            //}
        }
    }

    //only for showing debug messages
    private void ShowDebug(RaycastHit2D hit1, RaycastHit2D hit2)
    {
        Debug.DrawRay(new Vector3(wheel1.position.x, wheel1.position.y - 0.38f), Vector3.down, Color.red);
        Debug.DrawRay(new Vector3(wheel2.position.x, wheel2.position.y - 0.38f), Vector3.down, Color.red);
        message = "Hit 1: ";
        if (hit1.collider != null)
        {
            message += hit1.collider.name;
        }
        else
        {
            message += "null";
        }
        message += " Hit 2: ";
        if (hit2.collider != null)
        {
            message += hit2.collider.name;
        }
        else
        {
            message += "null ";
        }
        Debug.Log(message);
    }
}
