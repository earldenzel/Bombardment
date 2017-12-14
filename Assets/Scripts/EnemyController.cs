using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider HP;
    public bool suicide;
    private Text cameraMessage;

    void Start()
    {
        cameraMessage = GameObject.FindGameObjectWithTag("Environment").transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (HP.value == 0)
        {
            if (suicide)
            {
                //also have to announce suicide!!
                StartCoroutine(SuicideSequence());
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

    private IEnumerator SuicideSequence()
    {
        cameraMessage.text = this.gameObject.tag + " - " + this.gameObject.name + " has committed SUICIDE!";
        yield return new WaitForSeconds(3.0f);
        GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ProjectileController>() != null)
        {
            if (collision.gameObject.tag == "Scorch2" && GetComponent<FuelController>() != null)
            {
                GetComponent<FuelController>().UseFuel(2f);
            }

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
