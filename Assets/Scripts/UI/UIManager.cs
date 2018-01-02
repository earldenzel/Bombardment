using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    private int powerUpCounts;
    private Image[] imgPowerUps;
    void Start()
    {
        powerUpCounts = this.transform.GetChild(1).childCount;
        imgPowerUps = new Image[powerUpCounts];
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI(Tank tank, bool flashing)
    {
        PowerUpRepository repoPU = tank.PowerUpRepository;
        for (int i = 0; i < powerUpCounts; i++)
        {
            Transform trans = this.transform.GetChild(1).GetChild(i);
            if (i < repoPU.PowerUps.Length)
            {
                trans.gameObject.SetActive(true);
                trans.gameObject.GetComponent<Image>().sprite = repoPU.PowerUps[i].Sprite;
                if (flashing)
                {
                    if (repoPU.PowerUps[i].Type == PowerUp.PowerUpType.IncreaseDamage)
                    {
                        trans.GetComponent<ObjectEffect>().Enabled = true;
                        trans.GetComponent<ObjectEffect>().EnableFade = true;
                    }
                }
            }
            else{
                trans.gameObject.SetActive(false);
                trans.GetComponent<ObjectEffect>().Enabled = false;
                trans.GetComponent<ObjectEffect>().EnableFade = false;
            }
        }
    }
}
