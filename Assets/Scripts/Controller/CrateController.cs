using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour {
    
    public int MaxCratePerTurn = 5;
    public int MinCratePerTurn = 2;
    public GameObject[] crates;

    private float minSpawnX;
    private float maxSpawnX;

	// Use this for initialization
	void Start () {
        minSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x;
        maxSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x + GameManager.Instance.GameData.CameraSettings.ViewPort.width;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.C))
        {
            createCrates();
        }
    }

    IEnumerator SpawnCrate()
    {
        yield return new WaitForSeconds(3);
        createCrates();
    }

    void createCrates()
    {
        int numOfCrate = Random.Range(MinCratePerTurn, MaxCratePerTurn);

        for (int i = 0; i < numOfCrate; i++)
        {
            Instantiate(crates[Random.Range(0, crates.Length)], new Vector3(Random.Range(minSpawnX, maxSpawnX), 10, 0), Quaternion.identity);
        }
    }



}
