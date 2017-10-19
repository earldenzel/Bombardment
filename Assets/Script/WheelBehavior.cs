using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : MonoBehaviour {

    private Rigidbody2D parentBody;
    private bool onFreefall; //activates when on freefall
	// Use this for initialization
	void Start () {
        parentBody = transform.root.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //void OnCollisionStay2D(Collision2D collision)
    //{
    //    parentBody.freezeRotation = false;

    //} 
    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    parentBody.freezeRotation = true;
    //}
}
