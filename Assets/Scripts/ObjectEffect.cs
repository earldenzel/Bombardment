using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectEffect : MonoBehaviour {

    public enum EffectType { ZoomInAndOut, ZoomIn, ZoomOut, RotateClockwise, RotateCounterClockwise, FadeInAndOut, FadeIn, FadeOut, Flip }

    public EffectType[] Effects;

    public bool Enabled;
    public float MaxSize = 2;
    public float MinSize = 0.5f;
    public float ZoomSpeed = 0.5f;
    public float RotateSpeed = 20;
    public float FadeSpeed = 0.1f;

    public bool oneTurnFlip;
    public float FlipSpeed = 20;
    public Text text;

    public bool EnableFade;
    public bool fadeSpriteColor = true;
    public bool fadeTextColor = false;

    public bool ResetAlpha = false;

    private bool zoomTrigger = false;
    private bool fadeTrigger = false;
    public float alphaValue = 1;


    

	// Use this for initialization
	void Start () {
        if (ResetAlpha)
        {
            alphaValue = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Enabled)
        {
            foreach (EffectType effect in Effects)
            {
                switch (effect)
                {
                    case EffectType.ZoomInAndOut:
                        if (zoomTrigger)
                        {
                            zoomIn();
                        }
                        else
                        {
                            zoomOut();
                        }
                        break;
                    case EffectType.ZoomIn:
                        break;
                    case EffectType.ZoomOut:
                        zoomOut();
                        break;
                    case EffectType.RotateClockwise:
                        rotate(Vector3.back);
                        break;
                    case EffectType.RotateCounterClockwise:
                        rotate(Vector3.forward);
                        break;
                    case EffectType.FadeInAndOut:
                        if (fadeTrigger)
                        {
                            fadeIn();
                        }
                        else
                        {
                            fadeOut();
                        }
                        break;
                    case EffectType.FadeIn:
                        fadeIn();
                        break;
                    case EffectType.FadeOut:
                        fadeOut();
                        break;
                    case EffectType.Flip:
                        flip();
                        break;
                    default:
                        break;
                }
            }
        }
	}

    public void SetAlpha(float value)
    {
        if (fadeSpriteColor && this.GetComponent<SpriteRenderer>() != null)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, value);
        }
        if (fadeSpriteColor && this.GetComponent<Image>() != null)
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, value);
        }
        if (fadeTextColor && this.GetComponent<Text>() != null)
        {
            this.GetComponent<Text>().color = new Color(this.GetComponent<Text>().color.r, this.GetComponent<Text>().color.g, this.GetComponent<Text>().color.b, value);
        }
    }

    private void zoomIn()
    {
        if (transform.localScale.x >= MinSize)
        {
            transform.localScale -= (Vector3.right + Vector3.up ) * Time.deltaTime * ZoomSpeed;
        }
        else
        {
            zoomTrigger = !zoomTrigger;
        }
    }

    private void zoomOut()
    {
        if (transform.localScale.x <= MaxSize)
        {
            transform.localScale += (Vector3.right + Vector3.up) * Time.deltaTime * ZoomSpeed;
        }
        else
        {
            zoomTrigger = !zoomTrigger;
        }
    }

    private void fade()
    {
        if (fadeSpriteColor && this.GetComponent<SpriteRenderer>() != null)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alphaValue);
        }
        if (fadeSpriteColor && this.GetComponent<Image>() != null)
        {
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, alphaValue);
        }
        if (fadeTextColor && this.GetComponent<Text>() != null)
        {
            this.GetComponent<Text>().color = new Color(this.GetComponent<Text>().color.r, this.GetComponent<Text>().color.g, this.GetComponent<Text>().color.b, alphaValue);
        }
    }

    private void fadeIn()
    {
        if (EnableFade)
        {
            if (alphaValue - FadeSpeed * Time.deltaTime >= 0)
            {
                alphaValue -= FadeSpeed * Time.deltaTime;
                fade();
            }
            else
            {
                fadeTrigger = !fadeTrigger;
            }
        }
    }

    private void fadeOut()
    {
        if (EnableFade)
        {
            if (alphaValue + FadeSpeed * Time.deltaTime <= 1)
            {
                alphaValue += FadeSpeed * Time.deltaTime;
                fade();
            }
            else
            {
                fadeTrigger = !fadeTrigger;
            }
        }
    }

    private void rotate(Vector3 rot)
    {
        transform.Rotate(rot * RotateSpeed * Time.deltaTime);
    }

    private void flip()
    {
        Vector3 rot = Vector3.back * Time.deltaTime * FlipSpeed;
        if(text != null && rot.y >= 90)
        {            text.text = "";
        }
        rot.y = oneTurnFlip ? Mathf.Clamp(rot.y, 0, 90) : rot.y;
        transform.Rotate(rot);
    }
}
