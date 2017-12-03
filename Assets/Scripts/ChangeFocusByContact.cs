using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocusByContact : MonoBehaviour
{
    public float DelayTime = 1;
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
            ChangeFocusAndDestroy();
        }
    }

    public void ChangeFocusAndDestroy()
    {
        CameraController controller = Camera.main.GetComponent<CameraController>();
        controller.CameraDelay(DelayTime);
        //controller.SetCameraState(); // default fixed
        landed = true;
        Destroy(this.gameObject);
    }
}
