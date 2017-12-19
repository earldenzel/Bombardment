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
    private int activePlayerIndex;
    public int ActivePlayerIndex
    {
        get
        {
            return activePlayerIndex;
        }
        set
        {
            if(value < Players.Count)
            {
                activePlayerIndex = value;
            }
            else if (value < 0)
            {
                activePlayerIndex = 0;
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
    public List<GameObject> Players { get; }
    public CameraSetting CameraSettings { get; }

    public GameData()
    {
        Players = new List<GameObject>();
        SelectedTankClass = new List<Tank.Class>();
        CameraSettings = new CameraSetting();
        CameraSettings.ViewPort = new Rect(0, 0, 16, 8);
        ActivePlayerIndex = 0;
    }
}



public class GameManager : MonoBehaviour
{

    public enum TankType { Archer, Boomer, Pirate, Scorch }

    public int MIN_PLAYER = 4;
    public int MAX_PLAYER = 4;


    public static GameManager Instance;

    public GameData GameData { get; }

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
        GameData = new GameData();
    }

    public void AddPlayer(GameObject player)
    {
        GameData.Players.Add(player);
    }

    public void ResetPlayers()
    {
        GameData.Players.Clear();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
