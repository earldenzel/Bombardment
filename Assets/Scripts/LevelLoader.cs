using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //Check if current scene has any player
        if(players.Length > 0)
        {
            GameManager.Instance.ResetPlayers();
            for(int i = 0; i < players.Length; i++)
            {
                players[i].tag = "Player" + (i + 1);
                GameManager.Instance.AddPlayer(players[i]);
            }
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
            {
                GameObject go = GameManager.Instance.GameData.Players[i];
                go.tag = "Player" + (i + 1);
                Instantiate(go, new Vector3(Random.Range(-10, 10), 10, 0), Quaternion.identity);
            }
        }
    }

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
