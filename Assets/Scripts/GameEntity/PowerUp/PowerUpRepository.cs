using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRepository{

    private Dictionary<PowerUp.PowerUpType, PowerUp> powerUps;
    private Tank tank;

    private List<PowerUp.PowerUpType> expiredPowerUps;

    // Use this for initialization
    public PowerUpRepository(Tank tank)
    {
        this.tank = tank;
        powerUps = new Dictionary<PowerUp.PowerUpType, PowerUp>();
        expiredPowerUps = new List<PowerUp.PowerUpType>();
    }

    public void AddPowerUp(PowerUp pu)
    {
        PowerUp temp = null;
        if (powerUps.TryGetValue(pu.Type, out temp))
        {
            //Try to stack 2 power up
            Debug.Log("Power Up: " + pu.Type + " already exist.");
            temp += pu;
        }
        else
        {
            Debug.Log("Power Up: " + pu.Type + " has been added.");
            powerUps.Add(pu.Type, pu);
        }
    }

    //Called when turn enter
    public void OnTurnEnter()
    {
        foreach(PowerUp pu in powerUps.Values)
        {
            pu.OnApplyEnter(tank);
        }
        LifeTimeCheck();
    }

    public void OnTurnExcute()
    {
        foreach (PowerUp pu in powerUps.Values)
        {
            pu.OnApply(tank);
        }
        LifeTimeCheck();
    }

    //Called then turn exit
    public void OnTurnExit()
    {
        foreach (PowerUp pu in powerUps.Values)
        {
            pu.OnApplyExit(tank);
        }
        LifeTimeCheck();
    }

    //Remove all the power ups with 0 life time
    private void LifeTimeCheck()
    {
        foreach (PowerUp pu in powerUps.Values)
        {
            //If no more turns add this to expiredPowerUps
            if(pu.LifeTime == 0)
            {
                expiredPowerUps.Add(pu.Type);
            }
        }
        //remove
        foreach(PowerUp.PowerUpType type in expiredPowerUps)
        {
            RemovePowerUp(type);
        }
        //Reset expiredPowerUps list;
        expiredPowerUps.Clear();
    }

    public void RemovePowerUp(PowerUp.PowerUpType type)
    {
        powerUps.Remove(type);
        Debug.Log("Power Up: " + type + " has been removed.");
    }

    public void RemoveAll()
    {
        powerUps.Clear();
        Debug.Log("All Power Ups have been removed.");
    }

    public PowerUp[] PowerUps
    {
        get
        {
            PowerUp[] temp = new PowerUp[powerUps.Values.Count];
            int counter = 0;
            foreach(PowerUp pu in powerUps.Values)
            {
                temp[counter] = pu;
                counter++;
            }
            return temp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
