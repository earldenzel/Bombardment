﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour{

    public enum PowerUpType { IncreaseDamage, Fuel, Repair }
    public enum ApplyOn { NextTurn, Immediately }

    public string PowerUpName;
    public Sprite Sprite;
    public float Value;
    public bool EnableModifier;
    public bool EnableStack;
    public PowerUpType Type;
    public ApplyOn On;

    //Turn based life time or apply it immediately
    public int LifeTime = 1;

    public void OnApplyEnter(Tank applyTo)
    {
        //if the power up has turn based type then it will apply on the next turn
        if(On == ApplyOn.NextTurn)
        {
            switch (Type)
            {
                case PowerUpType.IncreaseDamage:
                    applyTo.DamageModifier = Value;
                 //   Debug.Log("New damage modifier: " + applyTo.DamageModifier);
                    break;
                case PowerUpType.Fuel:
                    applyTo.CurrentFuelLevel += Value;
                    break;
                case PowerUpType.Repair:
                    applyTo.CurrentHipPoint += Value;
                    break;
            }
            LifeTime -= 1;
            GameManager.Instance.StageController.UIManager.GetComponent<UIManager>().UpdateUI(applyTo, true);
        //    Debug.Log("Power Up: " + Type + " applied on turn enter");
        }
    }

    public void OnApply(Tank applyTo)
    {
        if (On == ApplyOn.Immediately)
        {
            switch (Type)
            {
                case PowerUpType.IncreaseDamage:
                    applyTo.DamageModifier = Value;
                    break;
                case PowerUpType.Fuel:
                    applyTo.CurrentFuelLevel += Value;
                    break;
                case PowerUpType.Repair:
                    applyTo.CurrentHipPoint += Value;
                    break;
            }
            LifeTime -= 1;
        }
        GameManager.Instance.StageController.UICanvas.GetComponent<CanvasController>().UpdateUI(applyTo.gameObject);
    }

    public void OnApplyExit(Tank applyTo)
    {
    }

    //This operation only stack the life time of the power up, 
    //if you want to modify the value use PowerUpModifier
    public static PowerUp operator +(PowerUp pu, PowerUp pu2)
    {
        if (pu.EnableStack)
        {
            pu.LifeTime += pu2.LifeTime;
            Debug.Log("Stacking Power Up: " + pu.Type + ", the life time now is : " + pu.LifeTime);
        }
        return pu;
    }

    //I dont know why I have a modifier class when I would just do PowerUp * int, 
    //but I think i will leave it like this.
    //Maybe adding restriction on the value the modifier can have
    public static PowerUp operator +(PowerUp pu, PowerUpModifier modifier)
    {
        pu.Value += modifier.Value;
        return pu;
    }

    //same as above
    public static PowerUp operator *(PowerUp pu, PowerUpModifier modifier)
    {
        pu.Value *= modifier.Value;
        return pu;
    }
}
