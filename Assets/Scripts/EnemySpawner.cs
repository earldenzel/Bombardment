using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    private List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
        //snow map tutorial coordinates used during first playable build
        //enemies.Add(Instantiate(enemy, new Vector3(5.8f, 6.84f), Quaternion.identity) as GameObject);
        //enemies.Add(Instantiate(enemy, new Vector3(13.0f, 3.0f), Quaternion.identity) as GameObject);
        //enemies.Add(Instantiate(enemy, new Vector3(25.4f, 10.92f), Quaternion.identity) as GameObject);
        //enemies.Add(Instantiate(enemy, new Vector3(16.4f, -3.8f), Quaternion.identity) as GameObject);
        //enemies.Add(Instantiate(enemy, new Vector3(-10.3f, 1.9f), Quaternion.identity) as GameObject);

        //tutorial map
        enemies.Add(Instantiate(enemy, new Vector3(25.32f, 3.95f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(23.28f, 11.08f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(38.9f, 3.81f), Quaternion.identity) as GameObject);
        enemies.Add(Instantiate(enemy, new Vector3(31.69f, 0.02f), Quaternion.identity) as GameObject);
        foreach (GameObject enemyTank in enemies)
        {
            //maybe show how many enemies left. gotta check the ui
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
