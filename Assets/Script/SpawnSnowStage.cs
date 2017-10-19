using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnowStage : MonoBehaviour {
    public GameObject ice;
    public GameObject snow;
    public GameObject ground;
    private float triangleSlope;
    private float pointSlope;

	// Use this for initialization
	void Start () {
        //CreateBlock(ice, -2, -2, 50, 0.96f);
        //CreateBlock(snow, -2, -3, 50, 0.96f);
        //CreateBlock(ice, 0, -1, 1, 0.5f);
        CreateTriangle(ice, -4f, -4f, 32f, 4f);
        //CreateTriangle2(ice, -1, 1, 1, 1);
        CreateBlock(ground, 28f, -8f, 8f, 4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateBlock(GameObject gameObject, float startx, float starty, float lengthx, float lengthy)
    {
        for(float i=0; i<lengthx; i += 0.16f)
        {
            for (float j=0; j<lengthy; j += 0.16f)
            {
                Instantiate(gameObject, new Vector3(startx+i, starty+j), Quaternion.identity);
            }
        }
    }

    void CreateTriangle(GameObject gameObject, float startx, float starty, float lengthx, float lengthy)
    {
        triangleSlope = lengthy / lengthx;
        for (float i = 0; i < lengthx; i += 0.16f)
        {
            for (float j = 0; j < lengthy; j += 0.16f)
            {
                pointSlope = j / i;
                if (pointSlope <= triangleSlope)
                {
                    Instantiate(gameObject, new Vector3(startx + i, starty + j), Quaternion.identity);
                }
            }
        }
    }
}
