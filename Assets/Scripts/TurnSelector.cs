using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSelector : MonoBehaviour {

    // Use this for initialization

    private List<GameObject> players;

    void Start()
    {
        ResetUI();
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetUI()
    {
        players = GameManager.Instance.GameData.Players;
        for (int i = 0; i < GameManager.Instance.MAX_PLAYER; i++)
        {
            Transform trans = this.transform.GetChild(i);
            if (i < GameManager.Instance.NumberOfPlayers)
            {
                trans.gameObject.SetActive(true);
                trans.GetComponent<Image>().sprite = GameManager.Instance.GameData.Players[i].GetComponent<Tank>().Sprite;
            }
            else
            {
                trans.gameObject.SetActive(false);
            }
            
        }
    }

    public void UpdateUI()
    {
        ResetUI();
        for (int i = 0; i < GameManager.Instance.NumberOfPlayers; i++)
        {
            Image image = this.transform.GetChild(i).GetComponent<Image>();
            if (i == GameManager.Instance.GameData.ActivePlayerIndex)
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
