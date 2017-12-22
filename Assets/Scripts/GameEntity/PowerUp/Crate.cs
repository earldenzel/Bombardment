using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Crate : MonoBehaviour {

    public float LifeTime = 3.0f;

    public int MaxHitPoint = 3;

    public bool SpwanPowerUp = true;

    private bool landed;

    private int hitPoint;
    public int HitPoint
    {
        get
        {
            return hitPoint;
        }

        set
        {
            if (value > 0)
            {
                hitPoint = value;
            }
            else
            {
                hitPoint = 0;
            }
            if (hpSlider != null)
            {
                hpSlider.value = hitPoint;
            }
        }
    }

    public Slider hpSlider;

    public GameObject[] powerUps;
	// Use this for initialization
	void Start () {
        // StartCoroutine(DecayHitPoint());
        HitPoint = MaxHitPoint;
        hpSlider.maxValue = HitPoint;
        hpSlider.value = hpSlider.maxValue;
        landed = false;
    }
	
	// Update is called once per frame

    public void SpawnPowerUps()
    {
        if (SpwanPowerUp && HitPoint == 0)
        {
            int numOfCrate = Random.Range(CrateManager.MinCratePerTurn, CrateManager.MaxCratePerTurn);

            for (int i = 0; i < numOfCrate; i++)
            {
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], this.transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator DecayHitPoint()
    {
        while (hpSlider.value > 0)
        {
            yield return new WaitForSeconds(1);
            hpSlider.value -= 1;
        }
    }

    public void ChangeColorByHitPoint()
    {
        if (hpSlider.value / hpSlider.maxValue <= 1 / hpSlider.maxValue)
        {
            hpSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            hpSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.green;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!landed)
        {
            this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            landed = true;
        }
        
    }
}
