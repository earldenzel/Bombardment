using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Slider HP;
    public bool suicide;
    public GameObject damageVisualizer;
    private Text cameraMessage;

    private Transform canvas;

    private Tank tank;

    void Start()
    {
        cameraMessage = GameObject.FindGameObjectWithTag("Environment").transform.GetChild(0).GetChild(0).GetComponent<Text>();
        canvas = GameObject.FindGameObjectWithTag("Environment").transform.GetChild(0);
        tank = GetComponent<Tank>();

    }

    void FixedUpdate()
    {
        if (tank.CurrentHipPoint == 0)
        {
            if (suicide)
            {
                //also have to announce suicide!!
                //StartCoroutine(SuicideSequence());
                GameManager.Instance.StageController.MakeAnnouncement(this.gameObject.tag + " - " + this.gameObject.name + " has committed SUICIDE!", 3);
                GameManager.Instance.GameData.ToNextPlayer = true;
            }
            Destroy(this.gameObject);
            //if (GetComponent<PlayerController>() != null)
            //{
            //    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
            //}
      //      GameManager.Instance.GameData.Players.Remove(this.gameObject);
            //GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
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

            Damage((int)(collision.gameObject.GetComponent<ProjectileController>().baseDamage * collision.gameObject.GetComponent<ProjectileController>().attacker.GetComponent<Tank>().DamageModifier), collision.gameObject);
            if (collision.gameObject.GetComponent<ProjectileController>().attacker == this.gameObject)
            {
//                Debug.Log(this.gameObject.name + " attacked itself");
                suicide = true;
            }
        }
    }

    public void Damage(int damage, GameObject target)
    {
        GameManager.Instance.StageController.MakeAnnouncement(this.gameObject.tag + " got hit by " + target.gameObject.GetComponent<ProjectileController>().attacker.tag + " and received " + damage + " damage.", 3);
        target.gameObject.GetComponent<ProjectileController>().attacker.GetComponent<Tank>().TotalDamageDealt += damage;
        //if (damageVisualizer!=null)
        //{
        //    GameObject go = Instantiate(damageVisualizer, target.transform.position, Quaternion.identity);
        //    go.transform.GetChild(0).GetComponent<Text>().text = damage.ToString();
        //    //go.transform.parent = GameObject.FindGameObjectWithTag("Environment").transform.GetChild(0);
        //}
        tank.CurrentHipPoint -= damage;
        HP.value = tank.CurrentHipPoint;
    }
}
