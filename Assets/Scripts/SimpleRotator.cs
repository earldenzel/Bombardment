using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameObject.tag == "Cleave")
        {
            transform.Rotate(0, 0, Random.Range(-25f, -15f));

        }
        else
        {
            transform.Rotate(0, 0, Random.Range(5f, 15f));
        }
	}
}
