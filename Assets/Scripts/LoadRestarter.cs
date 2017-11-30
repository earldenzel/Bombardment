using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LoadRestarter : MonoBehaviour {

    public string mainScene;
    public Text restartMessage;

    void Start()
    {
    }
    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RestartMessage());
        }
	}

    private IEnumerator RestartMessage()
    {
        if (restartMessage.GetComponent<ObjectEffect>())
        {
            restartMessage.GetComponent<ObjectEffect>().EnableFade = true;
        }
        restartMessage.text = "Restarting... hold on!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(mainScene);
    }
}
