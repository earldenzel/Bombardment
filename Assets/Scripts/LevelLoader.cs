using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private Transform[] spawningPoints;

    void Awake()
    {
        int numOfPoints = this.transform.childCount;
        spawningPoints = new Transform[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            spawningPoints[i] = this.transform.GetChild(i);
        }

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
            //Clear player list;
            GameManager.Instance.ResetPlayers();
            for (int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
            {
                Tank.Class tankClass = GameManager.Instance.GameData.SelectedTankClass[i];
                //Instantiate player based on prefab
                GameObject player = Instantiate(GameManager.Instance.GameData.TankPrefab[(int)tankClass], getValidPoint(), Quaternion.identity);
                player.tag = "Player" + (i + 1);
                //Add new player object to Players
                GameManager.Instance.GameData.Players.Add(player);
            }
        }
    }

    private Vector3 getValidPoint()
    {
        int pointIndex = Random.Range(0, spawningPoints.Length);
        Vector3 point = spawningPoints[pointIndex].position;
        spawningPoints[pointIndex].position.Set(point.x, point.y, -1);
        if (spawningPoints[pointIndex].position.z != 0)
        {
            getValidPoint();
        }
        return point;
    }
}
