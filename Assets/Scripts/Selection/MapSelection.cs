using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour {

    public Image imgMap;
    public Text txtMapDescription;
    public InputField inputMapName;
    public Button btnPreviousButton;
    public Button btnNextButton;
    public InputField inputNumberOfPlayer;
    public Button btnAdd;
    public Button btnRemove;
    public Sprite[] maps;

    private int numberOfPlayer;
    private int selectedMapIndex;
	// Use this for initialization
	void Start () {
        numberOfPlayer = GameManager.Instance.MIN_PLAYER;
        inputMapName.text = ((Map.Level)selectedMapIndex).ToString();
        inputNumberOfPlayer.text = GameManager.Instance.MIN_PLAYER.ToString();
        btnPreviousButton.interactable = false;
        btnRemove.interactable = false;
        mapUpdate();
    }

    public void PreviousMap()
    {
        if (selectedMapIndex > 0)
        {
            selectedMapIndex--;
            btnNextButton.interactable = true;
            if (selectedMapIndex == 0)
            {
                btnPreviousButton.interactable = false;
            }
        }
        inputMapName.text = ((Map.Level)selectedMapIndex).ToString();
        mapUpdate();
    }
    private void mapUpdate()
    {
        imgMap.sprite = maps[selectedMapIndex];
        if (selectedMapIndex == (int)Map.Level.Tutorial)
        {
            btnAdd.interactable = false;
            inputNumberOfPlayer.text = "1";
            numberOfPlayer = 1;
            btnRemove.interactable = false;
        }
        else
        {
            btnAdd.interactable = true;
            inputNumberOfPlayer.text = GameManager.Instance.MIN_PLAYER.ToString();
            numberOfPlayer = GameManager.Instance.MIN_PLAYER;
            if(numberOfPlayer == GameManager.Instance.MAX_PLAYER)
            {
                btnAdd.interactable = false;
            }
        }
    }

    public void NextMap()
    {
        if (selectedMapIndex < maps.Length - 1)
        {
            selectedMapIndex++;
            btnPreviousButton.interactable = true;
            if (selectedMapIndex == maps.Length - 1)
            {
                btnNextButton.interactable = false;
            }
        }
        inputMapName.text = ((Map.Level)selectedMapIndex).ToString();
        mapUpdate();
    }

    public void IncreaseNumberOfPlayer()
    {
        if (numberOfPlayer < GameManager.Instance.MAX_PLAYER)
        {
            numberOfPlayer++;
            btnRemove.interactable = true;
            if (numberOfPlayer == GameManager.Instance.MAX_PLAYER)
            {
                btnAdd.interactable = false;
            }
        }
        inputNumberOfPlayer.text = numberOfPlayer.ToString();
    }

    public void DecreaseNumberPlayer()
    {
        if(numberOfPlayer > GameManager.Instance.MIN_PLAYER)
        {
            numberOfPlayer--;
            btnAdd.interactable = true;
            if (numberOfPlayer == GameManager.Instance.MIN_PLAYER)
            {
                btnRemove.interactable = false;
            }
        }
        inputNumberOfPlayer.text = numberOfPlayer.ToString();
    }

    //Set current map and number of player in GameManager
    public void ConfrimSetup()
    {
        GameManager.Instance.NumberOfPlayers = numberOfPlayer;
        GameManager.Instance.GameData.SelectedMapIndex = selectedMapIndex;
        SceneManager.LoadScene("TankSelection");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
