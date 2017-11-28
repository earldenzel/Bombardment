using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroRotation : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.identity;
        transform.position = transform.root.position + 0.8f*Vector3.up;
	}
}
