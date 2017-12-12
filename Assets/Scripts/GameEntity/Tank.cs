using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    private PowerUpRepository powerUps;
    public string TankName;
    [TextArea]
    public string Description;

	void Start () {
        TankName = this.gameObject.name;
        powerUps = new PowerUpRepository();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyPowerUp()
    {

    }
}
