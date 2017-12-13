using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    private Text power;
    private Text shot;
    public Image canvasImage;
    private GameObject currentProjectile;
    public Text shotName;
    public Text shotStrength;
    public Text tankInfo;
    public Image tankImage;
    public List<Sprite> spriteList;
    public Slider hPSlider;
    public Slider fuelSlider;
    public Slider powerSlider;
    public Image anemometer;
    public Text windStrength;
    
    public void UpdateUI(GameObject currentPlayer)
    {
        //shows current shot
        if (currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().shot1)
        {
            currentProjectile = currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().cannon.shot1;
        }
        else
        {
            currentProjectile = currentPlayer.transform.GetChild(0).GetChild(0).GetComponent<CannonController>().cannon.shot2;
        }
        canvasImage.GetComponent<Image>().sprite = currentProjectile.GetComponent<SpriteRenderer>().sprite;
        canvasImage.GetComponent<Image>().preserveAspect = true;
        canvasImage.GetComponent<Image>().color = currentProjectile.GetComponent<SpriteRenderer>().color;
        shotName.text = currentProjectile.name;
        shotStrength.text = "Damage: " + currentProjectile.GetComponent<ProjectileController>().baseDamage
            + ((currentProjectile.name == "Triple Strafe") ? "x3" : "");

        //shows tank info
        tankInfo.text = currentPlayer.tag + "\n" + currentPlayer.name;
        switch (currentPlayer.name)
        {
            case "Archer":
                tankImage.GetComponent<Image>().sprite = spriteList[0];
                break;
            case "Boomer":
                tankImage.GetComponent<Image>().sprite = spriteList[1];
                break;
            case "Pirate":
                tankImage.GetComponent<Image>().sprite = spriteList[2];
                break;
            case "Scorch":
                tankImage.GetComponent<Image>().sprite = spriteList[3];
                break;
            default:
                break;
        }
        tankImage.GetComponent<Image>().preserveAspect = true;

        //show tank hp and fuel
        hPSlider.maxValue = currentPlayer.GetComponent<EnemyController>().HP.maxValue;
        hPSlider.value = currentPlayer.GetComponent<EnemyController>().HP.value;
        fuelSlider.maxValue = currentPlayer.GetComponent<FuelController>().fuelSlider.maxValue;
        fuelSlider.value = currentPlayer.GetComponent<FuelController>().fuelSlider.value;

    }
    public void UpdatePower(float currentPower)
    {
        powerSlider.value = currentPower;
    }

    public void UpdateWind(float angle, float power)
    {
        windStrength.text = "Wind Strength: " + power;
        anemometer.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,angle));
    }

    public void UpdateFuel(float currentFuel)
    {
        fuelSlider.value = currentFuel;
    }
}
