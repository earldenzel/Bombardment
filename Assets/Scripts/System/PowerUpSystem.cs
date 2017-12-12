using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour {

    public Dictionary<string, PowerUpRepository> powerUps;

	// Use this for initialization
	void Start () {
        powerUps = new Dictionary<string, PowerUpRepository>();
        for(int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
        {
            powerUps.Add("Player" + (i + 1), new PowerUpRepository());
        }
	}
}
