using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    private Text power;
    private Text shot;
    public Image canvasImage;
    private GameObject currentProjectile;
    //public Text powertext;
    public Text shotName;
    public Text shotStrength;

    // Use this for initialization
    void Start() {
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        //powertext = canvasObject.transform.Find("Powertxt").GetComponent<Text>();
        //shotText = canvasObject.transform.Find("Shottxt").GetComponent<Text>();
        //power.text = "Power: 90"; 
        //I changed the power setting temporarily to this text block so the teacher can safely navigate our game for the first playable
        //power.text = "CONTROLS"
        //    + "\n\tLeft or right directional keys - Move"
        //    + "\n\tUp and down directional keys - Increase/Decrease shot angle"
        //    + "\n\tTAB - Change shot type"
        //    + "\t\t\tRight CTRL - Jump forward"
        //    + "\n\tF - Toggle free camera"
        //    + "\t\t\tR - Reset the stage"
        //    + "\n\n\tPlease note that some elements at this stage are riddled with bugs";
    }    

    public void UpdateUI(GameObject currentPlayer)
    {
        if (currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shot1)
        {
            currentProjectile = currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().cannon.shot1;
        }
        else
        {
            currentProjectile = currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().cannon.shot2;
        }
        canvasImage.GetComponent<Image>().sprite = currentProjectile.GetComponent<SpriteRenderer>().sprite;
        canvasImage.GetComponent<Image>().color = currentProjectile.GetComponent<SpriteRenderer>().color;
        shotName.text = currentProjectile.name;
        shotStrength.text = "Damage: " + currentProjectile.GetComponent<ProjectileController>().baseDamage
            + ((currentProjectile.name == "Triple Strafe") ? "x3" : "");
    }
}
