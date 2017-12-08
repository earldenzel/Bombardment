using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankSelection : MonoBehaviour {

    public enum TankType { Archer, Boomer, Pirate, Scorch}

    //    public TankType[] tankList;
    public GameObject[] tanks;
    public Sprite[] tankSprites;
    public GridLayoutGroup selectedList;
    private TankType[] selectedTankList;
    private int selectedTankIndex = 0;
    private int currentPlayerIndex = 0;
    private int totalPlayer;
    // Use this for initialization

    public Text headerText;
    
	void Start () {
        selectedTankList = new TankType[GameManager.Instance.NumberOfPlayers];
        headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
    public void ConfirmSelectedTank()
    {
        selectedTankList[currentPlayerIndex] = (TankType)selectedTankIndex;
        selectedList.transform.GetChild(currentPlayerIndex).GetChild(0).GetComponent<Image>().sprite = tankSprites[selectedTankIndex];
        if (currentPlayerIndex < GameManager.Instance.NumberOfPlayers - 1)
        {
            currentPlayerIndex++;
            headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
        }
        else
        {
            headerText.text = "Get Ready!";
            AddTanksToGameManager();
            SceneManager.LoadSceneAsync("loading");
        }
    }

    private void AddTanksToGameManager()
    {
        for(int i = 0; i < selectedTankList.Length; i++)
        {
            GameObject go = tanks[(int)selectedTankList[i]];
            go.tag = "Player" + (i + 1);
            GameManager.Instance.AddPlayer(go);
        }
    }

    public void SetSelectedTank(int index)
    {
        if(index >= 0 && index < Enum.GetNames(typeof(TankType)).Length)
        {
            headerText.text = "Player " + (currentPlayerIndex + 1) + " selected " + (TankType)index;
            selectedTankIndex = index;
        }
    }
}
