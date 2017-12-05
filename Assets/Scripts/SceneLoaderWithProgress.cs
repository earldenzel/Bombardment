using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderWithProgress : MonoBehaviour {

    public Slider slider;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadAsyncScene());
	}

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("main");
        
        //Wait until the last operation fully loads to return anything
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
