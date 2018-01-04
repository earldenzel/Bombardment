using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour {

    public void Resume()
    {
        gameObject.SetActive(false);
        GameManager.Instance.StageController.Pause(false);
    }

    public void BackToMenu()
    {
        GameManager.Instance.StageController.Pause(false);
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
