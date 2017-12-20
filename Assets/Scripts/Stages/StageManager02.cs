using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager02 : MonoBehaviour, IStage {

    public Text text;

    public bool FirstEnter { get; private set; }

    // Use this for initialization
    void Awake () {
        FirstEnter = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnter()
    {
        if (FirstEnter)
        {
            FirstEnter = false;
            return;
        }
        else
        {
            this.GetComponentInParent<CrateManager>().SpawnCrates();
        }
        ObjectEffect oe = text.GetComponent<ObjectEffect>();
        oe.Enabled = true;
        oe.EnableFade = true;
        oe.fadeTextColor = true;
        oe.RotateSpeed = 100;
        oe.Effects = new ObjectEffect.EffectType[2];
        oe.Effects[0] = ObjectEffect.EffectType.ZoomOut;
        oe.Effects[1] = ObjectEffect.EffectType.FadeOut;
        
       // text.text = "Turn Enter";
    }

    public void OnStage()
    {
        //foreach(GameObject player in GameManager.Instance.GameData.Players)
        //{
        //    player.GetComponent<Tank>().Velocity += 1;
        //}
    }

    public void OnExit()
    {
        //if (!FirstEnter)
        //{
        //    foreach (GameObject player in GameManager.Instance.GameData.Players)
        //    {
        //        player.GetComponent<Tank>().MaxFuelLevel += 1;
        //    }
        //}
    }
}
