using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{

    public Vector3 StartPoint;
    public float Speed;
    public float Interval = 1.0f;
    public float MaxDropPerCycle;
    public bool DepolyDrop;
    public GameObject[] Drops;

    private IEnumerator coroutine;

    private int dropCount = 0;

    // Use this for initialization
    void Start()
    {
        this.transform.position = StartPoint;

        coroutine = DepolyDrops(Interval);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    private IEnumerator DepolyDrops(float interval)
    {
        while (DepolyDrop)
        {
            yield return new WaitForSeconds(interval);
            if (dropCount < MaxDropPerCycle)
            {
                GameObject drop = Drops[Random.Range(0, Drops.Length)];
                if (drop != null)
                {
                    Instantiate(drop, this.transform);
                    dropCount++;
                }
            }
        }
    }
}
