using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Canvascontroller : MonoBehaviour {

    Text power;
    Text shot;
    public Image arrow1;
    public Image arrow2;
    bool shot1;
    bool locked;

    // Use this for initialization
    void Start() {
        shot1 = true;
        GameObject canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        //arrow1 = GameObject.FindGameObjectWithTag("Shot1");
        //arrow2 = GameObject.FindGameObjectWithTag("Shot2");

        Transform ptexttr = canvasObject.transform.Find("Powertxt");
        Transform stexttr = canvasObject.transform.Find("Shottxt");
        power = ptexttr.GetComponent<Text>();
        //power.text = "Power: 90"; 
        //I changed the power setting temporarily to this text block so the teacher can safely navigate our game for the first playable
        power.text = "CONTROLS"
            + "\n\tLeft or right directional keys - Move"
            + "\n\tUp and down directional keys - Increase/Decrease shot angle"
            + "\n\tTAB - Change shot type"
            + "\t\t\tRight CTRL - Jump forward"
            + "\n\tF - Toggle free camera"
            + "\t\t\tR - Reset the stage"
            + "\n\n\tPlease note that some elements at this stage are riddled with bugs";
        shot = stexttr.GetComponent<Text>();
        shot1 = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            shot1 = !shot1;
        }

        if (shot1)
        {
            shot.text = "Current weapon: SINGLE SHOT";
            arrow1.enabled = true;
            arrow2.enabled = false;
        }
        else
        {
            shot.text = "Current weapon: TRIPLE STRAFE";
            arrow1.enabled = false;
            arrow2.enabled = true;
        }

    }
}
