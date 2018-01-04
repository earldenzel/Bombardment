using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AudioManager : MonoBehaviour {

    public AudioClip BGM_Current;

    public AudioClip[] BackgroundMusics;

    private int lastClipIndex;

    // Use this for initialization
    void Start () {
        lastClipIndex = GameManager.Instance.GameData.SelectedMapIndex;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {


    }

    public void ChangeBackgroundMusicBasedOnScene()
    {
        if (GameManager.Instance.GameData.SelectedMapIndex + 1 != lastClipIndex)
        {
            if (GameManager.Instance.GameData.SelectedMapIndex + 1 <= BackgroundMusics.Length && BackgroundMusics[GameManager.Instance.GameData.SelectedMapIndex + 1] != null)
            {
                BGM_Current = BackgroundMusics[GameManager.Instance.GameData.SelectedMapIndex + 1];
                lastClipIndex = GameManager.Instance.GameData.SelectedMapIndex + 1;
            }
            this.GetComponent<AudioSource>().clip = BGM_Current;
            this.GetComponent<AudioSource>().Play(0);
        }
        
    }
}
