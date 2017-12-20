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

    //UI panels
    public GameObject TurnSelector;
    public GameObject InGameMenuPanel;
    public GameObject GameResultPanel;

    void Awake () {
        
    }

    void Start()
    {
        //Before anything else, set controls to proper axes
        //players = new List<GameObject>();
        players = GameManager.Instance.GameData.Players;

        for (int i = 1; i <= GameManager.Instance.NumberOfPlayers; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player" + i.ToString());
            if (player != null)
            {
                player.name = player.name.Substring(0, player.name.IndexOf("(") < 0 ? player.name.Length : player.name.IndexOf("("));
                player.GetComponent<PlayerController>().horizontal = "Horizontal_P" + i.ToString();
                player.GetComponent<PlayerController>().jump = "Jump_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().vertical = "Vertical_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shoot = "Shoot_P" + i.ToString();
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().switchShot = "Switch_P" + i.ToString();
                //players.Add(player);
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
        ////show turn list
        //string message = players.Count + " players ready to fight. Turn list: ";
        //foreach (GameObject player in players)
        //{
        //    message += player.name + " ";
        //}
        //Debug.Log(message);
        playerCount = players.Count + enemies.Length;
        totalTurnsDone = -1;
        EnableNextPlayer();
        TurnSelector.GetComponent<TurnSelector>().ResetUI();
    }

    void Update()
    {
        if (gameOver)
        {
          //  showGameResult();
          //  return;
            StartCoroutine(GameFinish());
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            InGameMenuPanel.SetActive(!InGameMenuPanel.activeSelf);
        }
        //If menu isn't active update the game
        if (!InGameMenuPanel.activeSelf)
        {
            if (GameManager.Instance.GameData.ToNextPlayer)
            {
                GameManager.Instance.GameData.ToNextPlayer = false;
                EnableNextPlayer();

            }
            if (GameManager.Instance.GameData.SelectedMapIndex != 0 && GameManager.Instance.NumberOfPlayers == 1 && currentPlayer != null)
            {
             //   cameraMessage.text = "Game over! " + currentPlayer.tag + " (" + currentPlayer.name + ") wins! Returning to Menu.";
                DisableEveryone();
                gameOver = true;
            //    Debug.Log(playerCount);
            }
            else if (GameManager.Instance.GameData.SelectedMapIndex != 0 && playerCount == 0 && currentPlayer == null)
            {
                cameraMessage.text = "Game over! DRAW!";
                gameOver = true;
            }
        }
    }

    private void showGameResult()
    {
        if (!GameResultPanel.activeSelf)
        {
            GameResultPanel.SetActive(true);
            Transform header = GameResultPanel.transform.GetChild(0).GetChild(0);
            Transform content = GameResultPanel.transform.GetChild(0).GetChild(1);
            Text playerName = header.GetChild(0).GetComponent<Text>();
            Image tankSprite = content.GetChild(0).GetComponent<Image>();
            Text tankName = content.GetChild(0).GetChild(0).GetComponent<Text>();
            Text description = content.GetChild(1).GetComponent<Text>();

            playerName.text += currentPlayer.tag;
            tankSprite.sprite = currentPlayer.GetComponent<Tank>().Sprite;
            tankName.text = currentPlayer.GetComponent<Tank>().name;
            description.text = "Total tank destroyed: ???";
        }
    }

    IEnumerator GameFinish()
    {
        showGameResult();
        yield return new WaitForSeconds(5.0f);
        GameManager.Instance.ResetPlayers();
        SceneManager.LoadScene("Menu");
    }

    public void EnableNextPlayer()
    {
        
        if (gameOver)
        {
            return;
        }
        totalTurnsDone++;
        //Check if the cycle just Enter;
        if (totalTurnsDone % players.Count == 0)
        {
            Debug.Log("Turn Enter");
            this.GetComponentInParent<StageManager02>().OnExit();
            this.GetComponentInParent<StageManager02>().OnEnter();
        }
        this.GetComponentInParent<StageManager02>().OnStage();
        DisableEveryone();
        currentPlayer = players[totalTurnsDone % players.Count];
        GameManager.Instance.GameData.ActivePlayerIndex = totalTurnsDone % players.Count;
        
        if (currentPlayer != null)
        {
            TurnSelector.GetComponent<TurnSelector>().UpdateUI();
            currentPlayer.GetComponent<FuelController>().ReplenishFuel();
            UICanvas.UpdateUI(currentPlayer);
            mainCamera.GetComponent<CameraController>().SetFocus(currentPlayer);
            StartCoroutine(announcePlayerTurn(currentPlayer));
            currentPlayer.GetComponent<OrientationChecker>().onTurn = true;
            currentPlayer.GetComponent<PlayerController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().InstantiateShot();
            currentPlayer.GetComponent<Tank>().PowerUpRepository.OnTurnEnter();
            //Change UI for turn
            
            //Debug.Log(currentPlayer.name + "'s turn.");
        }
        else
        {
            EnableNextPlayer();
        }        
    }

    public IEnumerator announcePlayerTurn(GameObject currentPlayer)
    {
        //if (cameraMessage.GetComponent<ObjectEffect>())
        //{
        //    cameraMessage.GetComponent<ObjectEffect>().EnableFade = true;
        //}
        //cameraMessage.text = currentPlayer.tag + "'s turn - " + currentPlayer.name;
     //   TurnSelector.GetComponent<TurnSelector>().ChangePlayer();
        yield return new WaitForSeconds(3f);
        //cameraMessage.text = "";
    }

    public void EndTurn()
    {
        GameManager.Instance.GameData.ToNextPlayer = true;
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
                player.GetComponent<OrientationChecker>().onTurn = false;
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = false;
            }
        }
    }

}