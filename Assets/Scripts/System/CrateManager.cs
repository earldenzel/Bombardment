using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateManager : MonoBehaviour {
    
    public static int MaxCratePerTurn = 2;
    public static int MinCratePerTurn = 1;

    public bool spawn;

    public GameObject Crate;
    public int MaxCratesPerGame = 5;
    

    private Transform[] spawningPoints;
  //  public GameObject[] crates;

  //  public Sprite[] PowerUpSprites;

    private float minSpawnX;
    private float maxSpawnX;

	// Use this for initialization
	void Awake () {
        //    minSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x;
        //   maxSpawnX = GameManager.Instance.GameData.CameraSettings.ViewPort.x + GameManager.Instance.GameData.CameraSettings.ViewPort.width;

        int numOfPoints = this.transform.GetChild(1).childCount;
        spawningPoints = new Transform[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            spawningPoints[i] = this.transform.GetChild(1).GetChild(i);
        }
    }

    public void SpawnCrates()
    {
        if (spawn && GameManager.Instance.GameData.NumberOfCratesOnMap < MaxCratesPerGame)
        {
            GameManager.Instance.StageController.MakeAnnouncement("Deploying crates...", 3);
            int numOfCrate = Random.Range(MinCratePerTurn, MaxCratePerTurn);

            for (int i = 0; i < numOfCrate; i++)
            {
                Instantiate(Crate, getPoint(), Quaternion.identity);
                GameManager.Instance.GameData.NumberOfCratesOnMap++;
            }
        }
        
    }

    private Vector3 getPoint()
    {
        int pointIndex = Random.Range(0, spawningPoints.Length);
        Vector3 point = spawningPoints[pointIndex].position;
        return point;
    }


}
