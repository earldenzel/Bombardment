using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private List<GameObject> players;
    public int totalTurnsDone;
    private int playerCount;    
    private GameObject[] enemies;
    private CanvasController UICanvas;
    public Text cameraMessage;
    private GameObject currentPlayer;
    public bool gameOver;
    public Camera mainCamera;

    void Awake () {
        
    }

    void Start()
    {
        //Before anything else, set controls to proper axes
        players = new List<GameObject>();

        for (int i = 1; i <= GameManager.Instance.NumberOfPlayers; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player" + i.ToString());
            player.name = player.name.Substring(0, player.name.Length - 7);
            if (player != null)
            {
                player.GetComponent<PlayerController>().horizontal = "Horizontal_P" + i.ToString();
                player.GetComponent<PlayerController>().jump = "Jump_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().vertical = "Vertical_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shoot = "Shoot_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().switchShot = "Switch_P" + i.ToString();
                players.Add(player);
            }
        }

        //determine game variables

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameOver = false;
        UICanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
        //shuffle player list
        for (int i = 0; i < players.Count; i++)
        {
            GameObject temp = players[i];
            int randomIndex = Random.Range(i, players.Count);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
        }
        //show turn list
        string message = players.Count + " players ready to fight. Turn list: ";
        foreach (GameObject player in players)
        {
            message += player.name + " ";
        }
        Debug.Log(message);
        playerCount = players.Count + enemies.Length;
        totalTurnsDone = -1;
        EnableNextPlayer();
    }

    void Update()
    {
        if (GameManager.Instance.GameData.ToNextPlayer)
        {
            GameManager.Instance.GameData.ToNextPlayer = false;
            EnableNextPlayer();
            
        }
        if (GameManager.Instance.GameData.SelectedMapIndex != 0 && playerCount == 1 && currentPlayer != null)
        {
            cameraMessage.text = "Game over! " + currentPlayer.tag + " (" + currentPlayer.name + ") wins! Returning to Menu.";
            DisableEveryone();
            gameOver = true;
            Debug.Log(playerCount);
        }
        else if (GameManager.Instance.GameData.SelectedMapIndex != 0 && playerCount == 0 && currentPlayer == null)
        {
            cameraMessage.text = "Game over! DRAW!";
            gameOver = true;
        }
        if (gameOver)
        {
            StartCoroutine(GameFinish());
        }
    }

    IEnumerator GameFinish()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Menu");
    }

    public void EnableNextPlayer()
    {
        if (gameOver)
        {
            return;
        }
        totalTurnsDone++;
        DisableEveryone();
        currentPlayer = players[totalTurnsDone % players.Count];
        if (currentPlayer != null)
        {
            currentPlayer.GetComponent<FuelController>().ReplenishFuel();
            UICanvas.UpdateUI(currentPlayer);
            mainCamera.GetComponent<CameraController>().cameraConfig.initialFocus = currentPlayer;
            mainCamera.GetComponent<CameraController>().ObjectTracer.Traget = currentPlayer;
            //mainCamera.GetComponent<CameraController>().ObjectTracer.Mode = ObjectTracerController.TraceMode.ZoomOut;
            StartCoroutine(announcePlayerTurn(currentPlayer));
            currentPlayer.GetComponent<PlayerController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().InstantiateShot();
            Debug.Log(currentPlayer.name + "'s turn.");
        }
        else
        {
            EnableNextPlayer();
        }
    }

    public IEnumerator announcePlayerTurn(GameObject currentPlayer)
    {
        if (cameraMessage.GetComponent<ObjectEffect>())
        {
            cameraMessage.GetComponent<ObjectEffect>().EnableFade = true;
        }
        cameraMessage.text = currentPlayer.tag + "'s turn - " + currentPlayer.name;
        yield return new WaitForSeconds(3f);
        cameraMessage.text = "";
    }

    public void ReducePlayers()
    {
        playerCount--;
    }

    private void DisableEveryone()
    {
        //disable all players
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().enabled = false;
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = false;
            }
        }
    }
}