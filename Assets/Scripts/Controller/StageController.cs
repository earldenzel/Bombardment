using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageController : MonoBehaviour, IStage
{

    //public List<GameObject> Players { get; private set; }
    public GameObject[] Players;
    private bool firstEnter;

    public bool FirstEnter
    {
        get
        {
            return firstEnter;
        }
    }

    public int totalTurnsDone;
    private int playerCount;
    private float time;
    private bool isEnablingPlayer;
    private GameObject[] enemies;
    private CanvasController UICanvasController;
    private GameObject currentPlayer;
    private GameObject lastPlayer;
    public Camera mainCamera;

    public bool hasObjective;
    public Objective objective;

    [TextArea]
    public string announcementAtStart;

    //UI panels
    public GameObject UICanvas;
    public GameObject TurnSelector;
    public GameObject UIManager;
    public GameObject InGameMenuPanel;
    public GameObject GameResultPanel;
    public GameObject AnnouncementPanel;
    public GameObject QuickGuidePanel;

    public GameObject BtnEndTurn;

    private LevelLoader levelLoader;

    /** Level Loader **/
    private Transform[] spawningPoints;
    private int loopCount;

    public int PlayerRemaining { get; private set; }

    private int currentPlayerIndex;
    public int CurrentPlayerIndex
    {
        get
        {
            return currentPlayerIndex;
        }
        set
        {
            if (value >= GameManager.Instance.MAX_PLAYER)
            {
                currentPlayerIndex = 0;
            }
            else
            {
                currentPlayerIndex = value;
            }
        }
    }

    public void UpdatePlayerRemaing()
    {
        int numberOfPlayers = 0;
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                numberOfPlayers++;
            }
        }
        PlayerRemaining = numberOfPlayers;
    }

    public void ClearPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = null;
        }
    }

    private void instantiatePlayers()
    {
        ClearPlayers();
        //Get Spawning Points;
        int numOfPoints = this.transform.GetChild(0).childCount;
        spawningPoints = new Transform[numOfPoints];
        for (int i = 0; i < numOfPoints; i++)
        {
            spawningPoints[i] = this.transform.GetChild(0).GetChild(i);
        }

        for (int i = 0; i < GameManager.Instance.MAX_PLAYER; i++)
        {
            if (i < GameManager.Instance.GameData.SelectedTankClass.Count)
            {
                Tank.Class tankClass = GameManager.Instance.GameData.SelectedTankClass[i];
                //Instantiate player based on prefab

                GameObject player = Instantiate(GameManager.Instance.GameData.TankPrefab[(int)tankClass], getValidPoint(), Quaternion.identity);

                player.tag = "Player" + (i + 1);
                //Set new player object to Players
                Players[i] = player;
            }
        }
        UpdatePlayerRemaing();
    }

    private Vector3 getValidPoint()
    {
        int pointIndex = Random.Range(0, spawningPoints.Length);
        Vector3 point = spawningPoints[pointIndex].position;
        //if this point have not been choose return this point
        if (spawningPoints[pointIndex].position.z == 0)
        {
            //set z position so that this point will not be select again
            spawningPoints[pointIndex].position = new Vector3(point.x, point.y, -1);
            return point;
        }
        loopCount++;
        if (loopCount >= 10)
        {
            return point;
        }
        return getValidPoint();
    }

    /** Level Loader **/

    void Awake()
    {
        Players = new GameObject[4]; //GameManager.Instance.GameData.Players;

        instantiatePlayers();

        //Set controls to proper axes
        for (int i = 1; i <= PlayerRemaining; i++)
        {
            GameObject player = Players[i - 1];
            player.name = player.name.Substring(0, player.name.IndexOf("(") < 0 ? player.name.Length : player.name.IndexOf("("));
            player.GetComponent<PlayerController>().horizontal = "Horizontal_P" + i.ToString();
            player.GetComponent<PlayerController>().jump = "Jump_P" + i.ToString();
            player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().vertical = "Vertical_P" + i.ToString();
            player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shoot = "Shoot_P" + i.ToString();
            player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().switchShot = "Switch_P" + i.ToString();
        }
    }

    void Start()
    {
        //determine game variables
        UICanvasController = UICanvas.GetComponent<CanvasController>();
        //shuffle player list
        for (int i = 0; i < PlayerRemaining; i++)
        {
            GameObject temp = Players[i];
            int randomIndex = Random.Range(i, PlayerRemaining);
            Players[i] = Players[randomIndex];
            Players[randomIndex] = temp;
        }

        totalTurnsDone = 0;
        firstEnter = true;
        GameManager.Instance.StageController = this;
        StartCoroutine(ShowQuickGuide());
    }

    public void MakeAnnouncement(string message, float delay)
    {
        StartCoroutine(makeAnnouncement(message, delay));
    }

    IEnumerator makeAnnouncement(string message, float delay)
    {
        AnnouncementPanel.gameObject.SetActive(true);
        AnnouncementPanel.transform.GetChild(0).GetComponent<Text>().text = message;
        yield return new WaitForSeconds(delay);
        //AnnouncementPanel.GetComponent<ObjectEffect>().SetAlpha(1);
        //AnnouncementPanel.GetComponent<ObjectEffect>().Enabled = true;
        //AnnouncementPanel.GetComponent<ObjectEffect>().EnableFade = true;
        AnnouncementPanel.gameObject.SetActive(false);
        AnnouncementPanel.transform.GetChild(0).GetComponent<Text>().text = "";
    }

    void Update()
    {
        //Show In Game Menu
        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    InGameMenuPanel.SetActive(!InGameMenuPanel.activeSelf);
        //}

        if (Input.GetKeyUp(KeyCode.G) && !firstEnter)
        {
            QuickGuidePanel.SetActive(!QuickGuidePanel.activeInHierarchy);
        }

        //If menu isn't active update the game
        if (!InGameMenuPanel.activeInHierarchy)
        {
            if (GameManager.Instance.GameData.ToNextPlayer)
            {
                GameManager.Instance.GameData.ToNextPlayer = false;
                //does not call another player if currently enabling another player
                if (!isEnablingPlayer && GameManager.Instance.GameData.TotalProjectile == 0)
                {
                    GameManager.Instance.GameData.ToNextPlayer = true;
                    StartCoroutine(EnableNextPlayer());
                }
            }
        }

        if (currentPlayer != null)
        {
            if (currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().onShot)
            {
                BtnEndTurn.GetComponent<Button>().interactable = false;
            }
        }

        if (currentPlayer != null)
        {
            OnStage();
        }
    }

    private void showGameResult(bool isDraw)
    {
        if (!GameResultPanel.activeSelf)
        {
            GameResultPanel.SetActive(true);
            Transform header = GameResultPanel.transform.GetChild(0).GetChild(0);
            Transform content = GameResultPanel.transform.GetChild(0).GetChild(1);
            Text headerText = header.GetChild(0).GetComponent<Text>();
            if (GameManager.Instance.GameData.SelectedMapIndex != 0)
            {
                Image tankSprite = content.GetChild(0).GetComponent<Image>();
                Text tankName = content.GetChild(0).GetChild(0).GetComponent<Text>();
                Text description = content.GetChild(1).GetComponent<Text>();

                if (isDraw)
                {
                    //Show draw result
                    headerText.text = "Draw";
                    tankSprite.gameObject.SetActive(false);
                    tankName.gameObject.SetActive(false);
                    description.gameObject.SetActive(false);
                }
                else
                {
                    headerText.text = "Congratulations! " + currentPlayer.tag;
                    tankSprite.sprite = currentPlayer.GetComponent<Tank>().Sprite;
                    tankName.text = "MVP: " + currentPlayer.GetComponent<Tank>().name;
                    int totalDamageDealt = (int)currentPlayer.GetComponent<Tank>().TotalDamageDealt;
                    description.text = "Total damage dealt: " + totalDamageDealt;
                }
            }
            else
            {
                headerText.text = "Tutorial Ends";
                content.gameObject.SetActive(false);
            }
            
        }
    }    

    IEnumerator GameFinish(bool isDraw)
    {
        showGameResult(isDraw);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Menu");
    }

    public bool IsGameOver()
    {
        if (hasObjective && objective.Achieved)
        {
            return true;
        }
        if (GameManager.Instance.GameData.SelectedMapIndex == 0 || PlayerRemaining > 1)
        {
            return false;
        }
        return true;
    }

    private int nextAlivePlayerIndex()
    {
        //check all player after current player
        for(int i = CurrentPlayerIndex; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                //Debug.Log("After: " + i);
                return i;
            }
        }
        //check all player before current player
        for (int i = 0; i < CurrentPlayerIndex; i++)
        {
            if (Players[i] != null)
            {
                //Debug.Log("Before: " + i);
                return i;
            }
        }
        //no player alive means draw
        return -1;
    }

    private int firstAlivePlayer()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                return i;
            }
        }
        return -1;
    }

    public IEnumerator EnableNextPlayer()
    {
        isEnablingPlayer = true;
        if (!FirstEnter)
        {
            yield return new WaitForSeconds(1f);
            BtnEndTurn.GetComponent<Button>().interactable = true;
        }
        //wind changes every 8 turns, irregardless of what happens
        totalTurnsDone++;
        if ((totalTurnsDone % 8) == 0){
            GameObject.FindGameObjectWithTag("Environment").GetComponent<WindSpawner>().ChangeWind();
        }

        UpdatePlayerRemaing();        

        CurrentPlayerIndex = nextAlivePlayerIndex();
        if (CurrentPlayerIndex != -1)
        {
            currentPlayer = Players[CurrentPlayerIndex];
        }
        if (IsGameOver())
        {
            StartCoroutine(GameFinish(CurrentPlayerIndex < 0));
            //return;
        }
        //Check Winning Condition

        DisableEveryone();
        //Check if the cycle just Enter;
        if (CurrentPlayerIndex == firstAlivePlayer())
        {
            OnExit();
            OnEnter();
        }

        if (lastPlayer != null)
        {
            currentPlayer.GetComponent<Tank>().PowerUpRepository.OnTurnExit();
        }

        lastPlayer = currentPlayer;
        

        if (CurrentPlayerIndex != -1)
        {
            currentPlayer.GetComponent<Tank>().PowerUpRepository.OnTurnEnter();
            TurnSelector.GetComponent<TurnSelector>().UpdateUI();
            if (UIManager != null)
            {
                UIManager.GetComponent<UIManager>().UpdateUI(currentPlayer.GetComponent<Tank>(), false);
            }
            currentPlayer.GetComponent<FuelController>().ReplenishFuel();
            UICanvasController.UpdateUI(currentPlayer);
            mainCamera.GetComponent<CameraController>().SetFocus(currentPlayer);
            currentPlayer.GetComponent<OrientationChecker>().onTurn = true;
            currentPlayer.GetComponent<PlayerController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = true;
            currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().InstantiateShot();
        }
    
        CurrentPlayerIndex++;
        isEnablingPlayer = false;

    }

    public void EndTurn()
    {
        if (currentPlayer != null)
        {
            GameManager.Instance.GameData.ToNextPlayer = true;
            isEnablingPlayer = false;
            MakeAnnouncement(currentPlayer.tag + " decided to end current turn.", 3);
        }
        
    }

    private void DisableEveryone()
    {
        //disable all players
        for(int i = 0; i < Players.Length; i++)
        {
            GameObject player = Players[i];
            if (player != null)
            {
                player.GetComponent<PlayerController>().enabled = false;
                player.GetComponent<OrientationChecker>().onTurn = false;
                player.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().enabled = false;
            }            
        }
    }

    public void EliminatePlayer(int id)
    {
        int index = -1;
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null && Players[i].GetComponent<Tank>().ID == id)
            {
                index = i;
            }
        }
        if (index != -1)
        {
            Players[index] = null;
        }
    }

    public void OnEnter()
    {
        if (FirstEnter)
        {
            firstEnter = false;
            return;
        }
        else
        {
            this.GetComponentInParent<CrateManager>().SpawnCrates();
        }
    }

    IEnumerator ShowQuickGuide()
    {
        QuickGuidePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        QuickGuidePanel.SetActive(false);

        if (GameManager.Instance.GameData.SelectedMapIndex == 0)
        {
            MakeAnnouncement("Destroy all the crates", 5);
        }
        else
        {
            //Announce turn list
            string message = PlayerRemaining + " players ready to fight. Turn list: ";
            foreach (GameObject player in Players)
            {
                if (player != null)
                {
                    message += player.tag + " ";
                }

            }

            MakeAnnouncement(message, 5);
        }
        

        StartCoroutine(EnableNextPlayer());
    }

    public void OnStage()
    {
        if (GameManager.Instance.GameData.SelectedMapIndex == 0)
        {
            currentPlayer.GetComponent<Tank>().CurrentHipPoint = currentPlayer.GetComponent<Tank>().MaxHitPoint;
            currentPlayer.GetComponent<Tank>().CurrentFuelLevel = currentPlayer.GetComponent<Tank>().MaxFuelLevel;
        }
    }

    public void OnExit()
    {
        
    }
}