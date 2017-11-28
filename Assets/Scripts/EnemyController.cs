using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider HP;

    void FixedUpdate()
    {
        if (HP.value == 0)
        {
            Destroy(this.gameObject);
            //after this is where you instantiate the explosion
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ProjectileController>() != null)
        {
            HP.value -= collision.gameObject.GetComponent<ProjectileController>().baseDamage;
        }
    }
}
