using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleIndicator : MonoBehaviour {

    private LineRenderer arc;
    public float radius = 1f;
    private float xOffset;
    private float yOffset;
    private float x;
    private float y;
    private int startAngle;
    private int endAngle;

	void Start () {
        arc = GetComponent<LineRenderer>();
        List<Vector3> arcPoints = new List<Vector3>();
        startAngle = (int)transform.GetChild(0).GetComponent<CannonController>().cannon.minAngle;
        endAngle = (int) transform.GetChild(0).GetComponent<CannonController>().cannon.maxAngle;
        xOffset = transform.GetChild(0).position.x - transform.position.x;
        yOffset = transform.GetChild(0).position.y - transform.position.y;

        for (int i = startAngle; i <= endAngle; i++)
        {
            if (this.tag == "Scorch")
            {
                y = Mathf.Sin((i + 90) * Mathf.Deg2Rad) * radius + yOffset;
                x = Mathf.Cos((i + 90) * Mathf.Deg2Rad) * radius + xOffset;
            }
            else
            {
                y = Mathf.Sin(i * Mathf.Deg2Rad) * radius + yOffset;
                x = Mathf.Cos(i * Mathf.Deg2Rad) * radius + xOffset;
            }
            arcPoints.Add(new Vector3(x, y, 1));
        }
        Vector3[] positions = new Vector3[endAngle - startAngle + 1];
        positions = arcPoints.ToArray();
        arc.positionCount = positions.Length;
        arc.SetPositions(positions);
    }
}
