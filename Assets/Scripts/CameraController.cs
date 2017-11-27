using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraConfig
{
    public enum CameraState { ZoomIn, ZoomOut, Fixed, Center, CenterZoomOut };
    public CameraState State;
    public float MaxSizeOnObject;
    public float MaxSizeOnScreen;
    public float MinSize;
    public float ZoomRatio;
    public float ReturnSpeed;
}

public class CameraController : MonoBehaviour
{

    public enum Target { Player, Projectile };

    public CameraConfig cameraConfig;
    public GameObject player;
    public GameObject projectile;
    public GameObject target;
    public Target targetState;
    private Vector3 offset;
    public Vector3 centerOffset;

    // Use this for initialization
    void Start()
    {
        offset = new Vector3(0, 0, transform.position.z);
        if (player != null)
        {
            target = player;
        }
    }

    void LateUpdate()
    {
        switch (targetState)
        {
            case Target.Player:
                if (player != null)
                {
                    target = player;
                }
                
                break;
            case Target.Projectile:
                if (player != null)
                {
                    target = projectile;
                }
                break;
        }
        switch (cameraConfig.State)
        {
            case CameraConfig.CameraState.ZoomIn:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - cameraConfig.ZoomRatio, cameraConfig.MinSize, cameraConfig.MaxSizeOnScreen);
                break;
            case CameraConfig.CameraState.ZoomOut:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + cameraConfig.ZoomRatio, cameraConfig.MinSize, cameraConfig.MaxSizeOnObject);
                break;
            case CameraConfig.CameraState.Fixed:
                Camera.main.orthographicSize = cameraConfig.MaxSizeOnObject;
                break;
            case CameraConfig.CameraState.Center:
                centerOffset = Vector3.zero - player.transform.position;
                cameraConfig.State = CameraConfig.CameraState.CenterZoomOut;
                break;
            case CameraConfig.CameraState.CenterZoomOut:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + cameraConfig.ZoomRatio, cameraConfig.MinSize, cameraConfig.MaxSizeOnScreen);
                if (Camera.main.orthographicSize == cameraConfig.MaxSizeOnScreen)
                {
                    centerOffset = Vector3.zero;
                    cameraConfig.State = CameraConfig.CameraState.ZoomIn;
                }
                break;
        }
        if(target != null)
        {
            transform.position = target.transform.position + offset + centerOffset;
        }
    }
}