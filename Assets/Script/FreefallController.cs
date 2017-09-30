using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefallController : MonoBehaviour {
    private RaycastHit2D hit1;
    private RaycastHit2D hit2;
    private RaycastHit2D hit3;
    private Rigidbody2D tankVehicle;
    private bool controllable;
    private bool grounded;

    public bool Controllable
    {
        get
        {
            return controllable;
        }
    }

    // Use this for initialization
    void Start () {
        tankVehicle = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        hit1 = Physics2D.Raycast(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.5f), new Vector3(0, -1, 0), 0.05f);
        hit2 = Physics2D.Raycast(new Vector3(transform.GetChild(1).position.x, transform.GetChild(1).position.y - 0.5f), new Vector3(0, -1, 0), 0.05f);
        hit3 = Physics2D.Raycast(new Vector3(transform.GetChild(2).position.x, transform.GetChild(2).position.y), new Vector3(0, -1, 0), 0.05f);
        
        tankVehicle.AddForce(-20 * transform.up); // force that "sticks" the tank to the land
        if(tankVehicle.rotation >90 || tankVehicle.rotation < -90 || (hit1.collider == null && hit2.collider == null && hit3.collider == null))
        {
            CorrectPosition();
            controllable = false;
        }
        
        if (hit1.collider != null || hit2.collider != null || hit3.collider != null)
        {
            controllable = true;
        }
    }

    private void CorrectPosition()
    {
        transform.up = new Vector3(0, 1, 0);
        tankVehicle.rotation = 0;

    }
}
