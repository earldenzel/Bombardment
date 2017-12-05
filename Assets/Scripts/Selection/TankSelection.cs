using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TankSelection : MonoBehaviour {

    public enum TankType { Archer, Boomer, Pirate}

//    public TankType[] tankList;
    private TankType[] selectedTank;
    private int currentTankIndex = 0;
    private int currentPlayerIndex = 0;
    private int totalPlayer;
    // Use this for initialization

    public Text headerText;
    
	void Start () {
        GameManager.Instance.AddPlayer(gameObject);
        selectedTank = new TankType[GameManager.Instance.NumberOfPlayers];
        headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void AddTankToSelection()
    {
        selectedTank[currentPlayerIndex] = (TankType)currentTankIndex;
        if(currentPlayerIndex < GameManager.Instance.NumberOfPlayers - 1)
        {
            currentPlayerIndex++;
            headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
        }
    }
}
