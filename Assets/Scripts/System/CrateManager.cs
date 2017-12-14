using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateManager : MonoBehaviour {
    
    public static int MaxCratePerTurn = 5;
    public static int MinCratePerTurn = 2;

    public bool spawn;

    public GameObject Crate;
  //  public GameObject[] crates;

  //  public Sprite[] PowerUpSprites;

    private float minSpawnX;
    private float maxSpawnX;

	// Use this for initialization
	void Start () {
        minSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x;
        maxSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x + GameManager.Instance.GameData.CameraSettings.ViewPort.width;
    }

    public void SpawnCrates()
    {
        if (spawn)
        {
            int numOfCrate = Random.Range(MinCratePerTurn, MaxCratePerTurn);

            for (int i = 0; i < numOfCrate; i++)
            {
                Instantiate(Crate, new Vector3(Random.Range(minSpawnX, maxSpawnX), 10, 0), Quaternion.identity);
            }
        }
        
    }
/*
    public void SpawnCrates()
    {
        int numOfCrate = Random.Range(MinCratePerTurn, MaxCratePerTurn);

        for (int i = 0; i < numOfCrate; i++)
        {
            Instantiate(crates[Random.Range(0, crates.Length)], new Vector3(Random.Range(minSpawnX, maxSpawnX), 10, 0), Quaternion.identity);
        }
    }
*/


}
