using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    public int SelectedMapIndex = 0;

    public List<Tank.Class> SelectedTankClass;
    public GameObject[] TankPrefab;
    private int currentPlayerIndex;
    public int CurrentPlayerIndex
    {
        get
        {
            return currentPlayerIndex;
        }
        set
        {
            if(value >= GameManager.Instance.MAX_PLAYER)
            {
                currentPlayerIndex = 0;
            }
            else
            {
                currentPlayerIndex = value;
            }
        }
    }
    private int totalProjectile;
    public int TotalProjectile
    {
        set
        {
            if(value < 0)
            {
                totalProjectile = 0;
            }
            else
            {
                totalProjectile = value;
            }
        }
        get
        {
            return totalProjectile;
        }
    }
    public bool ToNextPlayer;

    public int NumberOfCratesOnMap;

    public GameObject[] Players { get; }
    public CameraSetting CameraSettings { get; }

    public GameData(int maxPlayer)
    {
        Players = new GameObject[maxPlayer];
        SelectedTankClass = new List<Tank.Class>();
        CameraSettings = new CameraSetting();
        CameraSettings.ViewPort = new Rect(0, 0, 16, 8);
        CurrentPlayerIndex = 0;
        NumberOfCratesOnMap = 0;
    }
}



public class GameManager : MonoBehaviour
{

    public enum TankType { Archer, Boomer, Pirate, Scorch }

    public int MIN_PLAYER = 4;
    public int MAX_PLAYER = 4;


    public static GameManager Instance;

    public StageController StageController;

    public GameData GameData { get; private set; }

    public int NumberOfPlayersOnStart;

    private int numberOfPlayers;

    public int NumberOfPlayers {
        get
        {
            return numberOfPlayers;
        }
        set
        {
            if (GameData.SelectedMapIndex == 0)
            {
                numberOfPlayers = 1;
            }
            if (value >= MIN_PLAYER && value <= MAX_PLAYER)
            {
                numberOfPlayers = value;
            }
        }
    }
    

    public GameManager()
    {
       
    }

    //public void AddPlayer(GameObject player)
    //{
    //    GameData.Players.Add(player);
    //}

    //public void RemoveActivePlayer()
    //{
    //    GameData.Players.RemoveAt(GameData.ActivePlayerIndex);
    //    numberOfPlayers--;
    //}

    //public void ResetPlayers()
    //{
    //    GameData.Players.Clear();
    //}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameData = new GameData(MAX_PLAYER);
        }
        else if (Instance != this)
        {
            DestroyObject(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
       
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
