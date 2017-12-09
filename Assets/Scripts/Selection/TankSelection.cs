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
        headerText.GetComponent<ObjectEffect>().EnableFade = true;
        int children = selectedList.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (i >= GameManager.Instance.NumberOfPlayers)
            {
                selectedList.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        updateSelectionUI();
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
            headerText.GetComponent<ObjectEffect>().EnableFade = true;
            headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
        }
        else
        {
            headerText.text = "Get Ready!";
            AddTanksToGameManager();
            SceneManager.LoadScene("Loading");
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

    private void updateSelectionUI()
    {
        Transform slot = selectedList.transform.GetChild(currentPlayerIndex);
        for (int i = 0; i < selectedTankList.Length; i++)
        {
            if (i == currentPlayerIndex)
            {
                slot.GetChild(1).GetComponent<ObjectEffect>().EnableFade = true;
            }
            else
            {
                slot.GetChild(1).GetComponent<ObjectEffect>().EnableFade = false;
                slot.GetChild(1).GetComponent<ObjectEffect>().SetAlpha(1);
            }
        }
    }

    public void SetSelectedTank(int index)
    {
        updateSelectionUI();
        if (index >= 0 && index < Enum.GetNames(typeof(TankType)).Length)
        {
            headerText.GetComponent<ObjectEffect>().SetAlpha(1);
            headerText.GetComponent<ObjectEffect>().EnableFade = false;
            headerText.text = "Player " + (currentPlayerIndex + 1) + " selected " + (TankType)index;
            selectedTankIndex = index;
            
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
