using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnowStage : MonoBehaviour {
    public GameObject ice;
    public GameObject snow;
	// Use this for initialization
	void Start () {
        CreateBlock(ice, -2, -2, 30, 1);
        CreateBlock(snow, -2, -3, 30, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateBlock(GameObject gameObject, int startx, int starty, float lengthx, float lengthy)
    {
        for(float i=0; i<lengthx; i += 0.08f)
        {
            for (float j=0; j<lengthy; j += 0.08f)
            {
                Instantiate(gameObject, new Vector3(startx+i, starty+j), Quaternion.identity);
            }
        }
    }
}
