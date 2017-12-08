using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameData
{
    public List<GameObject> Players;

    public GameData()
    {
        Players = new List<GameObject>();
    }
}

public class GameManager : MonoBehaviour
{

    public enum TankType { Archer, Boomer, Pirate }

    public static GameManager Instance;

    private GameData gameData;

    private int numberOfPlayers;
    public int NumberOfPlayers { get; private set; }
    

    public GameManager()
    {
        gameData = new GameData();
        NumberOfPlayers = 2;
    }

    public void AddPlayer(GameObject player)
    {
        gameData.Players.Add(player);
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

    // Update is called once per frame
    void Update()
    {

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
