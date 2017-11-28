using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    private List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
        enemies.Add(Instantiate(enemy, new Vector3(5.8f, 6.84f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(13.0f, 3.0f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(25.4f, 10.92f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(16.4f, -3.8f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(-10.3f, 1.9f), Quaternion.identity) as GameObject);

        foreach (GameObject enemyTank in enemies)
        {
            //maybe show how many enemies left. gotta check the ui
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
