using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraConfig
{
    public enum CameraState { ZoomIn, ZoomOut, Fixed };
    public CameraState State;
    public float MaxiumCameraSize;
    public float MinimumCameraSize;
    public float ZoomRatio;
}

public class CameraController : MonoBehaviour {

    public enum Target { Player, Projectile };

    public CameraConfig cameraConfig;
    public GameObject player;
    public GameObject projectile;
    public GameObject target;
    public Target targetState;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 0, transform.position.z);
        if (player != null)
        {
            target = player;
        }
	}
	
	void LateUpdate () {
        if (player != null && projectile != null)
        {
            switch (targetState)
            {
                case Target.Player:
                    target = player;
                    break;
                case Target.Projectile:
                    target = projectile;
                    break;
            }
        }
        switch (cameraConfig.State)
        {
            case CameraConfig.CameraState.ZoomIn:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - cameraConfig.ZoomRatio, cameraConfig.MinimumCameraSize, cameraConfig.MaxiumCameraSize);
                break;
            case CameraConfig.CameraState.ZoomOut:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + cameraConfig.ZoomRatio, cameraConfig.MinimumCameraSize, cameraConfig.MaxiumCameraSize);
                break;
            case CameraConfig.CameraState.Fixed:
                Camera.main.orthographicSize = cameraConfig.MaxiumCameraSize;
                break;
        }
        transform.position = target.transform.position + offset;

    }
}
