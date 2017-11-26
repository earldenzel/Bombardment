using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public enum GameScene { Loading, TankSelection, Main }

    public GameScene NextScene;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        switch (NextScene)
        {
            case GameScene.TankSelection:
                if (Input.anyKey)
                {
                    Debug.Log("Loading Selection");
                    SceneManager.LoadScene("selection");
                }
                break;
            case GameScene.Main:
                if (Input.GetKeyUp(KeyCode.KeypadEnter))
                {
                    Debug.Log("Loading Game");
                    SceneManager.LoadScene("main");
                }
                break;
        }
	}
}
