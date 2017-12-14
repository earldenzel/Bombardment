using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class CameraSetting
{
    private readonly int minViewPortX = 3;
    private readonly int maxViewPortX = 15;
    private readonly int minViewPortY = 3;
    private readonly int maxViewPortY = 15;
    private Rect viewPort;

    public int value;

    public Rect ViewPort
    {
        get
        {
            return viewPort;
        }

        set
        {
            viewPort.x = -(value.width / 2);
            viewPort.y = -(value.height / 2);
            viewPort.width = Mathf.Clamp(value.width, minViewPortX, maxViewPortX);
            viewPort.height = Mathf.Clamp(value.height, minViewPortY, maxViewPortY);
        }
    }


}
