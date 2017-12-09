using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    void Awake()
    {
        for (int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
        {
            GameObject go = GameManager.Instance.GameData.Players[i];
            go.tag = "Player" + (i + 1);
            Instantiate(go, new Vector3(Random.Range(-10, 10), 10, 0), Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
