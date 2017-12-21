using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocusByContact : MonoBehaviour
{
    public float DelayTime = 3;
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
        if (gameObject.tag == "Trebuchet" && other.gameObject.tag == "Trebuchet")
        {
            return;
        }
        if (other.gameObject.tag == "Crate")
        {
            other.gameObject.GetComponent<Crate>().HitPoint -= 1;
          //  Debug.Log(other.gameObject.GetComponent<Crate>().HitPoint);
            if(other.gameObject.GetComponent<Crate>().HitPoint == 0)
            {
                other.gameObject.GetComponent<Crate>().SpawnPowerUps();
                GameManager.Instance.GameData.NumberOfCratesOnMap--;
                Destroy(other.gameObject);
                return;
            }
        }
        if (reflectedTimes > 0)
        {
            reflectedTimes--;
            ReflectShot(projectileVelocity);
        }
        else
        {
            ChangeFocusAndDestroy();
        }
        //if (!landed)
        //{
            
        //}
    }

    private void ReflectShot(Vector2 lastVelocity)
    {
        GetComponent<Rigidbody2D>().AddForce(-lastVelocity.normalized, ForceMode2D.Impulse);
    }

    public void ChangeFocusAndDestroy()
    {
        CameraController controller = Camera.main.GetComponent<CameraController>();
        GameManager.Instance.GameData.TotalProjectile -= 1;
     //   Debug.Log(GameManager.Instance.GameData.TotalProjectile);
        controller.CameraDelay(DelayTime);
        //controller.SetCameraState(); // default fixed
        
    //    landed = true;
    
        Destroy(this.gameObject);
    }
}
