using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(EndExplosion());
    }

    IEnumerator EndExplosion()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
