using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocusByContact : MonoBehaviour
{
    public float WaitTime = 1;
    private bool landed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!landed)
        {
            Invoke("ChangeFocusAndDestroy", WaitTime);
        }
    }

    public void ChangeFocusAndDestroy()
    {
        CameraController controller = Camera.main.GetComponent<CameraController>();
        controller.ObjectTracker.SetFoucs(controller.GetPlayer(), ObjectTracerController.TraceState.Idle);
        //controller.SetCameraState(); // default fixed
        landed = true;
        Destroy(this.gameObject);
    }
}
