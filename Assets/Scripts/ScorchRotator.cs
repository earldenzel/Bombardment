using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchRotator : MonoBehaviour {

    private Rigidbody2D rb2d;
    private float angle;
    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //responsible for making arrow behave arrowlike
        if (rb2d.velocity != Vector2.zero)
        {
            rb2d.freezeRotation = true;
            transform.up = -rb2d.velocity;
        }
    }
}
