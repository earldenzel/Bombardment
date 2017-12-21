using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFalling : MonoBehaviour {

    private int dbf;

	// Use this for initialization
	void Start () {
		if(GameManager.Instance.GameData.SelectedMapIndex == 2)
        {
            dbf = -50;
        }
        else
        {
            dbf = -10;
        }
	}
	
	void LateUpdate () {
        if (transform.position.y < dbf)
        {
            if (gameObject.GetComponent<ChangeFocusByContact>() != null)
            {
                gameObject.GetComponent<ChangeFocusByContact>().ChangeFocusAndDestroy();
            }
            else
            {
                // GameManager.Instance.StageController.EliminatePlayer(this.transform.GetComponent<Tank>().ID);
                GameManager.Instance.StageController.MakeAnnouncement(gameObject.tag + " falling of the map!", 3);
                GameManager.Instance.GameData.ToNextPlayer = true;
                Destroy(this.gameObject);
                //this means the enemy or the gameplayer dieded. Focus on its destruction should be put here
                //insert script to temporarily focus here
                //if (gameObject.GetComponent<PlayerController>() != null)
                //{
                //    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
                //    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
                //}
                // GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();

                //   

                //   GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
                //    GameManager.Instance.StageController.RemovePlayer(this.gameObject);
                //    
            }
        }
    }
}
