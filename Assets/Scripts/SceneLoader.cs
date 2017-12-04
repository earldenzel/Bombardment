using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public enum GameScene { Loading, TankSelection, Main }

    public bool EnableTimeTransition;
    public float timeToNextScene = 3f;
    public GameScene NextScene;

	// Use this for initialization
	void Start () {
        if (EnableTimeTransition)
        {
            StartCoroutine(ToNextScene(NextScene.ToString(), timeToNextScene));
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            StartCoroutine(ToNextScene(NextScene.ToString(), 0));
        }
	}

    IEnumerator ToNextScene(string name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }
}
