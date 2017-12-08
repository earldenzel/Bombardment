using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderWithProgress : MonoBehaviour {

    public float delayBeforeLoad = 3f;
    public Slider slider;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadAsyncScene());
	}

    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        AsyncOperation operation = SceneManager.LoadSceneAsync(((Map.Level)GameManager.Instance.GameData.SelectedMapIndex).ToString());
        
        //Wait until the last operation fully loads to return anything
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
