using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public enum GameScene { Logo, Loading, Menu, StageSelection, TankSelection, Tutorial, Level1, Level2, Level3 }

    public bool EnableTimeTransition;
    public float timeToNextScene = 3f;
    public GameScene NextScene;

	// Use this for initialization
	void Start () {
        if (EnableTimeTransition)
        {
            StartCoroutine(ToNextScene(NextScene, timeToNextScene));
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator ToNextScene(GameScene name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name.ToString());
    }

    public void LoadScene(int level)
    {
        StartCoroutine(ToNextScene((GameScene)level, 0));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
