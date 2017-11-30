using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {


    public bool EnableParallaxScrolling;
    public float speedX = 0.3f;
    public float speedY = 0.3f;

    private Transform transCamera;

    private float lastCameraX;
    private float lastCameraY;

    void Start()
    {
        transCamera = Camera.main.transform;
        lastCameraX = transCamera.position.x;
        lastCameraY = transCamera.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (EnableParallaxScrolling)
        {
            float deltaX = transCamera.position.x - lastCameraX;
            float deltaY = transCamera.position.y - lastCameraY;
            transform.position += Vector3.right * (deltaX * speedX) + Vector3.up * (deltaY * speedY);

            lastCameraX = transCamera.position.x;
            lastCameraY = transCamera.position.y;
        }
    }
}
