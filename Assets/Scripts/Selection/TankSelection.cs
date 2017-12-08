using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankSelection : MonoBehaviour {

    public enum TankType { Archer, Boomer, Pirate, Scorch}

    //    public TankType[] tankList;
    public Sprite[] tankSprites;
    public GridLayoutGroup selectedList;
    private TankType[] selectedTankList;
    private int selectedTankIndex = 0;
    private int currentPlayerIndex = 0;
    private int totalPlayer;
    // Use this for initialization

    public Text headerText;
    
	void Start () {
        GameManager.Instance.AddPlayer(gameObject);
        selectedTankList = new TankType[GameManager.Instance.NumberOfPlayers];
        headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    
    public void ConfirmSelectedTank()
    {
        selectedTankList[currentPlayerIndex] = (TankType)selectedTankIndex;
        if (currentPlayerIndex < GameManager.Instance.NumberOfPlayers)
        {
            selectedList.transform.GetChild(currentPlayerIndex).GetChild(0).GetComponent<Image>().sprite = tankSprites[selectedTankIndex];
            currentPlayerIndex++;
            if(currentPlayerIndex == GameManager.Instance.NumberOfPlayers)
            {
                SceneManager.LoadScene("loading");
            }
            headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
