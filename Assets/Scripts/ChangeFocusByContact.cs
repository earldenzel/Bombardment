using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocusByContact : MonoBehaviour
{

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
            Camera.main.GetComponent<CameraController>().targetState = CameraController.Target.Player;
            Camera.main.GetComponent<CameraController>().cameraConfig.State = CameraConfig.CameraState.Fixed;
            landed = true;
        }
    }
}
