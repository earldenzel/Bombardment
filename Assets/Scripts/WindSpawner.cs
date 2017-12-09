using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour {

    private Vector3 wind;
    private float windPower;
    private float windAngle;
    public int maxWindPower;

    public Vector3 Wind
    {
        get
        {
            return wind;
        }
    }

    // Use this for initialization
    void Start ()
    {
        windPower = Random.Range(0, maxWindPower);
        wind = Random.onUnitSphere * windPower;
        windAngle = FindDegree(Wind.y, Wind.x);
        UpdateWind();
    }

    private void UpdateWind()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>().UpdateWind(windAngle,windPower);
    }

    float FindDegree(float y, float x)
    {
        float value = (float)((Mathf.Atan2(y, x) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }
}
