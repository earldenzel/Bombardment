using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFalling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10)
        {
            if (gameObject.GetComponent<ChangeFocusByContact>() != null)
            {
                gameObject.GetComponent<ChangeFocusByContact>().ChangeFocusAndDestroy();
            }
            else
            {
                //this means the enemy or the gameplayer dieded. Focus on its destruction should be put here
                //insert script to temporarily focus here
                if (gameObject.GetComponent<PlayerController>() != null)
                {
                    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().ReducePlayers();
                    GameObject.FindGameObjectWithTag("Environment").GetComponent<GameController>().EnableNextPlayer();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
