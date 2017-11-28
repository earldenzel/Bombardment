using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraConfig
{
    public enum CameraState { ZoomIn, ZoomOut, Fixed, Center, CenterZoomOut, Free };
    public CameraState State = CameraState.Fixed;
    public float SizeOnObjectFocused = 5;
    public float MaxOnScreenSize = 5; 
    public float MinZoomInSize = 3;
    public float ZoomRatio = 0.2f;
    public float FreeMovementSpeed = 0.3f;
    public Rect Boundary = new Rect(-15, 10, 50, 10);
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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            switch (cameraConfig.State)
            {
                case CameraConfig.CameraState.Free:
                    cameraConfig.State = CameraConfig.CameraState.Fixed;
                    offset.x = 0;
                    offset.y = 0;
                    break;
                case CameraConfig.CameraState.Fixed:
                    cameraConfig.State = CameraConfig.CameraState.Free;
                    break;
            }
            
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
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - cameraConfig.ZoomRatio, cameraConfig.MinZoomInSize, cameraConfig.MaxOnScreenSize);
                break;
            case CameraConfig.CameraState.ZoomOut:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + cameraConfig.ZoomRatio, cameraConfig.MinZoomInSize, cameraConfig.SizeOnObjectFocused);
                break;
            case CameraConfig.CameraState.Fixed:
                Camera.main.orthographicSize = cameraConfig.SizeOnObjectFocused;
                break;
            case CameraConfig.CameraState.Center:
                centerOffset = Vector3.zero - player.transform.position;
                cameraConfig.State = CameraConfig.CameraState.CenterZoomOut;
                break;
            case CameraConfig.CameraState.CenterZoomOut:
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + cameraConfig.ZoomRatio, cameraConfig.MinZoomInSize, cameraConfig.MaxOnScreenSize);
                if (Camera.main.orthographicSize == cameraConfig.MaxOnScreenSize)
                {
                    centerOffset = Vector3.zero;
                    cameraConfig.State = CameraConfig.CameraState.ZoomIn;
                }
                break;
            case CameraConfig.CameraState.Free:
                //Mouse Position Bottom Left = 0,0  /  Top Right = ScreenSize.x, ScreenSize.y
                if(Input.mousePosition.x < 0)
                {
                    //Restrict the camera to move outside of the map
                    if (transform.position.x - Camera.main.orthographicSize > cameraConfig.Boundary.x)
                    {
                        offset.x -= cameraConfig.FreeMovementSpeed;
                    }
                }
                else if(Input.mousePosition.x > Screen.width)
                {
                    if (transform.position.x + Camera.main.orthographicSize < cameraConfig.Boundary.x + cameraConfig.Boundary.width)
                    {
                        offset.x += cameraConfig.FreeMovementSpeed;
                    }
                }
                if (Input.mousePosition.y < 0)
                {
                    if (transform.position.y + Camera.main.orthographicSize > cameraConfig.Boundary.y - cameraConfig.Boundary.height)
                    {
                        offset.y -= cameraConfig.FreeMovementSpeed;
                    }
                }
                else if (Input.mousePosition.y > Screen.height)
                {
                    if (transform.position.y - Camera.main.orthographicSize < cameraConfig.Boundary.y)
                    {
                        offset.y += cameraConfig.FreeMovementSpeed;
                    }
                }
                break;
        }
        if(target != null)
        {
            switch (cameraConfig.State)
            {
                case CameraConfig.CameraState.Free:
                    transform.position = offset;
                    break;
                default:
                    transform.position = target.transform.position + offset + centerOffset;
                    break;
            }
        }
    }
}