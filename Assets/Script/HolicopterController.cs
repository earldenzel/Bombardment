using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolicopterController : MonoBehaviour {

    public Transform startPoint;
    public float speed;

	// Use this for initialization
	void Start () {
        this.transform.position = startPoint.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
}
