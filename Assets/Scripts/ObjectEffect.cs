using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEffect : MonoBehaviour {

    public enum EffectType { ZoomInAndOut, ZoomIn, ZoomOut, RotateClockwise, RotateCounterClockwise }

    public EffectType[] Effects;

    public bool Enabled;
    public float MaxSize = 2;
    public float MinSize = 0.5f;
    public float ZoomSpeed = 0.5f;
    public float RotateSpeed = 20;

    private bool zoomTrigger;

	// Use this for initialization
	void Start () {
		
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
                    default:
                        break;
                }
            }
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

    private void rotate(Vector3 rot)
    {
        transform.Rotate(rot * RotateSpeed * Time.deltaTime);
    }
}
