using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocusByContact : MonoBehaviour
{
    public float DelayTime = 1;
    private bool landed = false;
    private int reflectedTimes = 0;
    private Vector2 projectileVelocity;

    // Use this for initialization
    void Start()
    {
        if (transform.gameObject.tag == "Reflective")
        {
            reflectedTimes = 1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        projectileVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!landed)
        {
            if (reflectedTimes > 0)
            {
                reflectedTimes--;
                ReflectShot(projectileVelocity);
            }
            else
            {
                ChangeFocusAndDestroy();
            }
        }
    }

    private void ReflectShot(Vector2 lastVelocity)
    {
        GetComponent<Rigidbody2D>().AddForce(-lastVelocity.normalized, ForceMode2D.Impulse);
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
