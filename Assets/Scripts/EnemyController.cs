using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider HP;
    public bool suicide;

    void FixedUpdate()
    {
        if (HP.value == 0)
        {
            if (suicide)
            {
                GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
            }
            Destroy(this.gameObject);
            //if (GetComponent<PlayerController>() != null)
            //{
            //    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
            //}
            GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
            //after this is where you instantiate the explosion
        }
        suicide = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ProjectileController>() != null)
        {
            Damage(collision.gameObject.GetComponent<ProjectileController>().baseDamage);
            if (collision.gameObject.GetComponent<ProjectileController>().attacker == this.gameObject)
            {
                Debug.Log(this.gameObject.name + " attacked itself");
                suicide = true;
            }
        }
    }

    public void Damage(int damage)
    {
        Debug.Log(this.gameObject.name + " received " + damage + " damage.");
        HP.value -= damage;
    }
}
