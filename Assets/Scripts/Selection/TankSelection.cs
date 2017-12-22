using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankSelection : MonoBehaviour {

    //    public TankType[] tankList;
    public GameObject[] tanks;
    public Sprite[] tankSprites;
    public GridLayoutGroup selectedList;
    public GridLayoutGroup tankList;
    private Tank.Class[] selectedTankList;
    private int selectedTankIndex = 0;
    private int currentPlayerIndex = 0;
    private int totalPlayer;
    // Use this for initialization

    public Text headerText;
    public Text selectionHeaderText;
    
	void Start () {
        //Clear up selected tank
        GameManager.Instance.GameData.SelectedTankClass.Clear();
        selectedTankList = new Tank.Class[GameManager.Instance.NumberOfPlayers];
        headerText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
        headerText.GetComponent<ObjectEffect>().EnableFade = true;
        int children = selectedList.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            if (i >= GameManager.Instance.NumberOfPlayers)
            {
                selectedList.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        updateSelectionUI();
        //Set Prefab
        GameManager.Instance.GameData.TankPrefab = tanks;
    }
	
    private void setHeaderText()
    {
        selectionHeaderText.text = "Player " + (currentPlayerIndex + 1) + " is selecting...";
        selectionHeaderText.GetComponent<ObjectEffect>().EnableFade = true;
    }

    private void setSelectionHeaderText(int index)
    {
        selectionHeaderText.GetComponent<ObjectEffect>().SetAlpha(1);
        selectionHeaderText.GetComponent<ObjectEffect>().EnableFade = false;
        selectionHeaderText.text = "Player " + (currentPlayerIndex + 1) + " selected " + (Tank.Class)index;
        
    }

    // Update is called once per frame
    void Update () {
        
    }
    
    public void ConfirmSelectedTank()
    {
        selectedTankList[currentPlayerIndex] = (Tank.Class)selectedTankIndex;
        
        selectedList.transform.GetChild(currentPlayerIndex).GetChild(0).GetComponent<Image>().sprite = tankSprites[selectedTankIndex];
        if (currentPlayerIndex < GameManager.Instance.NumberOfPlayers - 1)
        {
            currentPlayerIndex++;
            setHeaderText();
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
            //Add Selected Tank Class
            GameManager.Instance.GameData.SelectedTankClass.Add(selectedTankList[i]);
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
        int children = tankList.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            if (tankList.transform.GetChild(i).gameObject.activeSelf)
            {
                if (i == index)
                {
                    tankList.transform.GetChild(i).GetComponent<ObjectEffect>().EnableFade = true;
                }
                else
                {
                    tankList.transform.GetChild(i).GetComponent<ObjectEffect>().EnableFade = false;
                    tankList.transform.GetChild(i).GetComponent<ObjectEffect>().SetAlpha(1);
                }
            }
        }
        if (index >= 0 && index < Enum.GetNames(typeof(Tank.Class)).Length)
        {
            setSelectionHeaderText(index);
            selectedTankIndex = index;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
