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
                //Instantiate(powerUps[0], this.transform.position, Quaternion.identity);
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], this.transform.position, Quaternion.identity);
            }
        }
    }

    public void ApplyDamageByHitCount(int hitCount, float tick)
    {
        StartCoroutine(DecayHitPoint(tick));
    }

    public void Decay(float tick)
    {
        StartCoroutine(DecayHitPoint(tick));
    }

    public void ApplyDamage(int damage)
    {
        if (hitPoint - damage > 0)
        {
            hitPoint -= damage;
        }
        else
        {
            hitPoint = 0;
        }
        ChangeColorByHitPoint();
    }

    IEnumerator DecayHitPoint(float tick)
    {
        while (HitPoint > 0)
        {
            yield return new WaitForSeconds(tick);
            HitPoint -= 1;
            hpSlider.value = hitPoint;
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
