using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotator : MonoBehaviour {

    private Rigidbody2D rb2d;
    private float angle;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //responsible for making arrow behave arrowlike
        if (rb2d.velocity != Vector2.zero)
        {
            rb2d.freezeRotation = true;
            angle = FindDegree(rb2d.velocity.y, rb2d.velocity.x);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.z = angle;
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    float FindDegree(float y, float x)
    {
        float value = (float)((Mathf.Atan2(y, x) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }
}
