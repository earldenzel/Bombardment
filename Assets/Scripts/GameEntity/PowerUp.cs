using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp {

    public enum PowerUpType { DamageIncrease, Fuel, Repair }

    public float Value { get; }
    public bool EnableMultipleModifier { get; }
    public int MultipleModifier { get; }

    public PowerUp()
    {
        
    }
}
