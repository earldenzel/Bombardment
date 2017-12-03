using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour {

    private GameObject tileMap;

	// Use this for initialization
	void Start () {
        tileMap = transform.GetChild(0).gameObject;
        tileMap.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
