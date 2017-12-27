using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour {

    private PlayerController thisPlayer;
    private CannonController thisCannon;
    public Slider fuelSlider;
    private CanvasController UICanvas;

    private Tank tank;

    void Awake()
    {
        tank = this.GetComponentInParent<Tank>();
    }

    // Use this for initialization
    void Start () {
        thisPlayer = GetComponent<PlayerController>();
        thisCannon = transform.GetChild(0).GetChild(0).GetComponent<CannonController>();
        UICanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
        fuelSlider.maxValue = tank.MaxFuelLevel;
        //tank = this.GetComponentInParent<Tank>();
    }
	
	void FixedUpdate () {
        if (thisPlayer.enabled)
        {
            //cost of moving is deltaTime
            if (Input.GetAxis(thisPlayer.horizontal) != 0)
            {
                if (Mathf.Abs(Input.GetAxis(thisPlayer.horizontal)) > 0.4f)
                {
                    UseFuel(Time.deltaTime / 2);
                }

            }

            //cost of jumping is 0.5 fuel units or less
            if (Input.GetButtonDown(thisPlayer.jump))
            {
                UseFuel(0.5f);
            }
        }

        if (thisCannon.enabled)
        {
            if(tank.CurrentFuelLevel < 1f)
            {
                thisCannon.canLoadStrongerShot = false;
                if (!thisCannon.onShot)
                {
                    thisCannon.LoadWeakerShot();
                }
            }
            else
            {
                thisCannon.canLoadStrongerShot = true;
            }
            
            if (Input.GetButtonDown(thisCannon.shoot) && !thisCannon.shot1 && thisCannon.onShot)
            {
                UseFuel(1f);
            }
        }
        if (tank.CurrentFuelLevel == 0f)
        {
            thisPlayer.enabled = false;
        }
        fuelSlider.value = tank.CurrentFuelLevel;
    }

    public void UseFuel(float fuel)
    {
        tank.CurrentFuelLevel -= fuel;
        if (thisCannon.enabled)
        {
            UICanvas.UpdateFuel(tank.CurrentFuelLevel);
        }
    }

    public void ReplenishFuel()
    {
        tank.CurrentFuelLevel += 1f;
    }
}
