using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour {

    private PlayerController thisPlayer;
    private CannonController thisCannon;
    public Slider fuelSlider;
    private CanvasController UICanvas;

	// Use this for initialization
	void Start () {
        thisPlayer = GetComponent<PlayerController>();
        thisCannon = transform.GetChild(0).GetChild(0).GetComponent<CannonController>();
        UICanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
    }
	
	void FixedUpdate () {
        if (thisPlayer.enabled)
        {
            //cost of moving is deltaTime
            if (Input.GetAxis(thisPlayer.horizontal) != 0)
            {
                UseFuel(Time.deltaTime / 2);
            }

            //cost of jumping is 0.5 fuel units or less
            if (Input.GetButtonDown(thisPlayer.jump))
            {
                UseFuel(0.5f);
            }
        }

        if (thisCannon.enabled)
        {
            if (fuelSlider.value < 1f)
            {
                thisCannon.canLoadStrongerShot = false;
                thisCannon.LoadWeakerShot();
            }
            else
            {
                thisCannon.canLoadStrongerShot = true;
            }

            if (Input.GetButtonDown(thisCannon.shoot) && !thisCannon.shot1)
            {
                UseFuel(1.0f);
            }
        }

        //disable movement when fuel = 0
        if (fuelSlider.value == 0f)
        {
            thisPlayer.enabled = false;
        }

    }

    public void UseFuel(float fuel)
    {
        fuelSlider.value -= fuel;
        UICanvas.UpdateFuel(fuelSlider.value);
    }

    public void ReplenishFuel()
    {
        fuelSlider.value += 1f;
    }
}
