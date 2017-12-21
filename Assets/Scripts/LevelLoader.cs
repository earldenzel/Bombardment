using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private Transform[] spawningPoints;

    private int loopCount;

    void Awake()
    {
        int numOfPoints = this.transform.childCount;
        spawningPoints = new Transform[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            spawningPoints[i] = this.transform.GetChild(i);
        }

        //Clear player list;
 //       GameManager.Instance.ResetPlayers();

        /** Testing Only - Remove/Comment after done **/

        //for (int i = 0; i < GameManager.Instance.MIN_PLAYER; i++)
        //{
        //    GameManager.Instance.GameData.SelectedTankClass.Add(Tank.Class.Archer);
        //}

        /****/

        for (int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
        {
            Tank.Class tankClass = GameManager.Instance.GameData.SelectedTankClass[i];
            //Instantiate player based on prefab
            GameObject player = Instantiate(GameManager.Instance.GameData.TankPrefab[(int)tankClass], getValidPoint(), Quaternion.identity);
            player.tag = "Player" + (i + 1);
            //Add new player object to Players
     //       GameManager.Instance.GameData.Players.Add(player);
        }
    }

    private Vector3 getValidPoint()
    {
        int pointIndex = Random.Range(0, spawningPoints.Length);
        Vector3 point = spawningPoints[pointIndex].position;
        //if this point have not been choose return this point
        if (spawningPoints[pointIndex].position.z == 0)
        {
            //set z position so that this point will not be select again
            spawningPoints[pointIndex].position = new Vector3(point.x, point.y, -1);
            return point;
        }
        loopCount++;
        if (loopCount >= 10)
        {
            return point;
        }
        return getValidPoint();
    }
}
