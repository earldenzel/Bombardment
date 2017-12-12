using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData
{
    public int SelectedMapIndex = 0;
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
    public Setting Settings { get; }

    public GameData()
    {
        Players = new List<GameObject>();
        Settings = new Setting();
    }
}

public class Setting
{
    private readonly int minViewPortX = 3;
    private readonly int maxViewPortX = 15;
    private readonly int minViewPortY = 3;
    private readonly int maxViewPortY = 15;
    private Rect viewPort = new Rect(0, 0, 16, 8);

    public Rect ViewPort {
        get
        {
            return viewPort;
        } 

        set
        {
            viewPort.x = -(value.width / 2);
            viewPort.y = -(value.height / 2);
            viewPort.width = Mathf.Clamp(value.width, minViewPortX, maxViewPortX);
            viewPort.height = Mathf.Clamp(value.height, minViewPortY, maxViewPortY);
        }
    }


}

public class GameManager : MonoBehaviour
{

    public enum TankType { Archer, Boomer, Pirate, Scorch }

    public readonly int MIN_PLAYER = 2;
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
            if(GameData.SelectedMapIndex == 0)
            {
                numberOfPlayers = 1;
            }
            if(value >= MIN_PLAYER && value <= MAX_PLAYER)
            {
                numberOfPlayers = value;
            }
        }
    }
    

    public GameManager()
    {
        GameData = new GameData();
        NumberOfPlayers = MIN_PLAYER;
    }

    public void AddPlayer(GameObject player)
    {

        GameData.Players.Add(player);
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
