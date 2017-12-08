using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    void Awake()
    {
        foreach (GameObject go in GameManager.Instance.GameData.Players)
        {
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
