using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroRotation : MonoBehaviour {

    private Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
    }
    
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.identity;
        if (transform.root.tag == "Enemy")
        {
            transform.position = transform.root.position + 0.8f * Vector3.up;
        }
        else
        {
            FlipBar(transform.root.GetComponent<OrientationChecker>().rightDirection);
            if (tag == "Health")
            {
                transform.position = transform.root.position - 1.35f * Vector3.up;
            }
            else if (tag == "Fuel")
            {
                transform.position = transform.root.position - 1.5f * Vector3.up;
            }
        }
	}
    private void FlipBar(bool rightDirection)
    {
        transform.localScale = new Vector3((rightDirection) ? originalScale.x : -originalScale.x, originalScale.y, originalScale.z);
    }
}
