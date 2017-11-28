using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFalling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10)
        {
            if (gameObject.GetComponent<ChangeFocusByContact>() != null)
            {
                gameObject.GetComponent<ChangeFocusByContact>().ChangeFocusAndDestroy();
            }
        }
    }
}
