using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private List<GameObject> players;
    public int totalTurnsDone;
    private int playerCount;
	
    //Before anything else, set controls to proper axes
	void Awake () {
        players = new List<GameObject>();
        for(int i=1; i<=4; i++)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player" + i.ToString());
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
    }

    void Start()
    {
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
        playerCount = players.Count;   
        totalTurnsDone = -1;
        EnableNextPlayer();
    }

    void Update()
    {
        if (playerCount == 1)
        {
            Debug.Log("Game over!");
        }
        else if (playerCount == 0)
        {
            Debug.Log("Draw");
        }
    }

    public void EnableNextPlayer()
    {
        totalTurnsDone++;
        DisableEveryone();
        GameObject currentPlayer = players[totalTurnsDone % players.Count];
        if (currentPlayer != null)
        {
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
                player.GetComponent<OrientationChecker>().freefall = true;
            }
        }
    }
}
