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

    public GameObject powerUps;
    public Sprite[] powerUpSprites;

    public void UpdateUI()
    {
        GameObject go = GameManager.Instance.StageController.Players[GameManager.Instance.StageController.CurrentPlayerIndex];
        UpdateUI(go);
    }

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
        string strength = "Damage: " + currentProjectile.GetComponent<ProjectileController>().baseDamage + (currentPlayer.GetComponent<Tank>().DamageModifier > 1 ? " * " + currentPlayer.GetComponent<Tank>().DamageModifier : "");
        if (currentProjectile.name == "Triple Strafe") strength += "x3";
        else if (currentProjectile.name == "Falling Stars") strength += "x?";
        shotStrength.text = strength;
        //shows tank info
        tankInfo.text = currentPlayer.tag + "\n" + currentPlayer.name;
        tankImage.GetComponent<Image>().sprite = currentPlayer.GetComponent<Tank>().Sprite;
        tankImage.GetComponent<Image>().preserveAspect = true;

        //show tank hp and fuel
        hPSlider.maxValue = currentPlayer.GetComponent<Tank>().MaxHitPoint;
        hPSlider.value = currentPlayer.GetComponent<Tank>().CurrentHipPoint;

        hPSlider.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = currentPlayer.GetComponent<Tank>().CurrentHipPoint + "/" + currentPlayer.GetComponent<Tank>().MaxHitPoint;

        fuelSlider.maxValue = currentPlayer.GetComponent<Tank>().MaxFuelLevel;
        fuelSlider.value = currentPlayer.GetComponent<Tank>().CurrentFuelLevel;

        fuelSlider.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = currentPlayer.GetComponent<Tank>().CurrentFuelLevel + "/" + currentPlayer.GetComponent<Tank>().MaxFuelLevel;
        
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

    public void UpdateFuel(float fuel)
    {
        fuelSlider.value = fuel;
    }
}
