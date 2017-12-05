using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;
	// Use this for initialization
	void Awake () {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        if (player1 != null)
        {
            player1.GetComponent<PlayerController>().horizontal = "Horizontal_P1";
            player1.GetComponent<PlayerController>().jump = "Jump_P1";
            player1.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().vertical = "Vertical_P1";
            player1.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shoot = "Shoot_P1";
            player1.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().switchShot = "Switch_P1";
            //set Inputs here
        }
        player2 = GameObject.FindGameObjectWithTag("Player2");
        if (player2 != null)
        {
            //set Inputs here
            player2.GetComponent<PlayerController>().horizontal = "Horizontal_P2";
            player2.GetComponent<PlayerController>().jump = "Jump_P2";
            player2.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().vertical = "Vertical_P2";
            player2.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shoot = "Shoot_P2";
            player2.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().switchShot = "Switch_P2";
        }
        //player3 = GameObject.FindGameObjectWithTag("Player3");
        //if (player3 != null)
        //{
        //    //set Inputs here
        //}
        //player4 = GameObject.FindGameObjectWithTag("Player4");
        //if (player1 != null)
        //{
        //    //set Inputs here
        //}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
