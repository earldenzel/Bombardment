using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
    
    public bool Achieved { get; private set; }

    public GameObject[] TargetsToDestroy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (allTargetDestroyed())
        {
            Achieved = true;
        }
	}

    private bool allTargetDestroyed()
    {
        for (int i = 0; i < TargetsToDestroy.Length; i++)
        {
            if (TargetsToDestroy[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
