using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefallController : MonoBehaviour {
    private RaycastHit2D hit1;
    private RaycastHit2D hit2;
    private RaycastHit2D hit3;
    private Rigidbody2D tankVehicle;
    private bool controllable;

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

        hit1 = Physics2D.Raycast(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y), -transform.up, 0.01f);
        hit2 = Physics2D.Raycast(new Vector3(transform.GetChild(1).position.x, transform.GetChild(1).position.y), -transform.up, 0.01f);
        hit3 = Physics2D.Raycast(new Vector3(transform.GetChild(2).position.x, transform.GetChild(2).position.y), -transform.up, 0.01f);
        //hit1 = Physics2D.Raycast(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 0.5f), new Vector3(0, -1, 0), 0.05f);
        //hit2 = Physics2D.Raycast(new Vector3(transform.GetChild(1).position.x, transform.GetChild(1).position.y - 0.5f), new Vector3(0, -1, 0), 0.05f);
        //hit3 = Physics2D.Raycast(new Vector3(transform.GetChild(2).position.x, transform.GetChild(2).position.y), new Vector3(0, -1, 0), 0.05f);
        Debug.DrawRay(new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y), -transform.up, Color.green);
        Debug.DrawRay(new Vector3(transform.GetChild(1).position.x, transform.GetChild(1).position.y), -transform.up, Color.green);
        Debug.DrawRay(new Vector3(transform.GetChild(2).position.x, transform.GetChild(2).position.y), -transform.up, Color.green);
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            tankVehicle.drag = 10;
            tankVehicle.position = Vector3.Lerp(tankVehicle.position, new Vector3(tankVehicle.position.x + 5f, tankVehicle.position.y + 5f), 0.8f);
        }
        else if (hit1.collider == null && hit2.collider == null && hit3.collider == null)
        {
            CorrectPosition();
            tankVehicle.position = Vector3.Lerp(transform.position, Physics2D.Raycast(new Vector3(transform.GetChild(2).position.x, transform.GetChild(2).position.y), -transform.up).point, 0.2f);

        }
        if (tankVehicle.rotation > 90 || tankVehicle.rotation < -90)
        {
            CorrectPosition();
            controllable = false;
        }

        if (hit1.collider != null || hit2.collider != null || hit3.collider != null)
        {
            controllable = true;
            tankVehicle.AddForce(-30 * transform.up); // force that "sticks" the tank to the land
        }
        
    }

    private void CorrectPosition()
    {
        transform.up = new Vector3(0, 1, 0);
        tankVehicle.rotation = 0;

    }
}
