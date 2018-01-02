using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

    public enum Class { Archer, Boomer, Pirate, Scorch, Gladiator, Trebuchet }

    private static uint nextAvaliableId;

    public int ID;

    public uint NextAvaliableId
    {
        get
        {
            return ++nextAvaliableId;
        }
    }

    private PowerUpRepository powerUps;
    //    public string TankName;

    [TextArea]
    public string Description;

    public Sprite Sprite;

    private float currentHipPoint;
    public float CurrentHipPoint
    {
        get
        {
            return currentHipPoint;
        }
        set
        {
            if (value <= 0)
            {
                currentHipPoint = 0;
            }
            else if (value > MaxHitPoint)
            {
                currentHipPoint = MaxHitPoint;
            }
            else
            {
                currentHipPoint = value;
            }
        //    Debug.Log("Current Hit Point : " + currentHipPoint);
        }
    }
    public float MaxHitPoint = 100;
    public float Velocity = 5;
    private float currentFuelLevel;
    public float CurrentFuelLevel
    {
        get
        {
            return currentFuelLevel;
        }
        set
        {
            if(value <= 0)
            {
                currentFuelLevel = 0;
            }
            else if (value > MaxFuelLevel)
            {
                currentFuelLevel = MaxFuelLevel;
            }
            else
            {
                currentFuelLevel = value;
            }
        }
    }

    public float MaxFuelLevel = 5;
    public float DamageModifier = 1;

    public GameObject Shot1;
    public int Shot1Count = 1;
    public GameObject Shot2;
    public int Shot2Count = 1;

    public float TotalDamageDealt;

    void Awake()
    {
        ID = (int)NextAvaliableId;
        powerUps = new PowerUpRepository(this);
    }

    void Start () {
        //    TankName = this.gameObject.name;
        currentHipPoint = MaxHitPoint;
        currentFuelLevel = MaxFuelLevel;

    }
	
	// Update is called once per frame
	void Update () {
		//Put code to decrease power up life time;
	}

    public PowerUpRepository PowerUpRepository
    {
        get
        {
            return powerUps;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            PowerUp pu = other.gameObject.GetComponent<PowerUp>();
            powerUps.AddPowerUp(pu);
            powerUps.OnTurnExcute();
            Destroy(other.gameObject);
            GameManager.Instance.StageController.UIManager.GetComponent<UIManager>().UpdateUI(this, false);
        }
    }

}
