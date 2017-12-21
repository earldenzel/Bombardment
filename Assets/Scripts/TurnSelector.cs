using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSelector : MonoBehaviour {
    
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetUI()
    {
        if (GameManager.Instance.StageController.Players != null)
        {
            GameObject[] players = GameManager.Instance.StageController.Players;
            //Set active sprite
            for (int i = 0; i < players.Length; i++)
            {
                Transform trans = this.transform.GetChild(i);
                if (players[i] != null)
                {
                    trans.gameObject.SetActive(true);
                    trans.GetComponent<Image>().sprite = players[i].GetComponent<Tank>().Sprite;
                }
                else
                {
                    trans.gameObject.SetActive(false);
                }

            }
        }

    }

    public void UpdateUI()
    {
        ResetUI();
        for (int i = 0; i < GameManager.Instance.StageController.Players.Length; i++)
        {
            Image image = this.transform.GetChild(i).GetComponent<Image>();
            if (i == GameManager.Instance.StageController.ActivePlayerIndex)
            {
                image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                image.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }
}
