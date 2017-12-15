using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelFriction : MonoBehaviour {

    private PhysicsMaterial2D wheelMaterial;
    private PlayerController thisPlayer;

	// Use this for initialization
	void Start () {
        wheelMaterial = GetComponent<CircleCollider2D>().sharedMaterial;
        thisPlayer = transform.root.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Mathf.Abs(Input.GetAxis(thisPlayer.horizontal)) <= 0.4f)
        {
            wheelMaterial.friction = 1f;
        }
        else
        {
            wheelMaterial.friction = 0.5f;
        }
	}
}
