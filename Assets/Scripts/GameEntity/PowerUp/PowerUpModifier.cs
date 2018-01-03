using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PowerUpModifier {

    public enum ApplyMode { Multiple, Addition }

    public ApplyMode Mode;

    public float Value { set;  get; }

    public PowerUpModifier(int value)
    {
        Value = value;
    }
}
