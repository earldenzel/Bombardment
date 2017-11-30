using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraConfig
{
    public enum CameraMode { Focus, Free };
    public CameraMode State = CameraMode.Focus;
    public GameObject initialFocus;
    public float MaxZoomingSize = 10;
    public float MinZoomingSize = 5;
    public float MouseWheelZoomingSpeed = 5;
    public float FreeMovementSpeed = 0.3f;
    public Rect CameraBoundary = new Rect(-15, 10, 50, 10);
}
[Serializable]
public class ObjectTracerController
{
    public enum TraceMode { None, ZoomIn, ZoomOut };
    public enum TraceState { Idle, Tracing };
    public bool Enabled;
    public TraceMode Mode = TraceMode.ZoomIn;
    public TraceState State;
    public GameObject Traget;
    public float MinSizeOnFocus = 2;
    public float MaxSizeOnFocus = 8;
    public float ZoomingSpeed = 0.1f;
    
    public void SetFoucs(GameObject target, TraceState state = TraceState.Tracing)
    {
        if (target != null)
        {
            Traget = target;
            State = state;
        }
    }
}

public class CameraController : MonoBehaviour
{
    
    public CameraConfig cameraConfig;
    public ObjectTracerController ObjectTracer;
    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = new Vector3(0, 0, transform.position.z);
        if (cameraConfig.initialFocus != null)
        {
            ObjectTracer.Traget = cameraConfig.initialFocus;
            player = cameraConfig.initialFocus;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            switch (cameraConfig.State)
            {
                case CameraConfig.CameraMode.Free:
                    cameraConfig.State = CameraConfig.CameraMode.Focus;
                    Camera.main.orthographicSize = (cameraConfig.MaxZoomingSize + cameraConfig.MinZoomingSize) / 2;
                    offset.x = 0;
                    offset.y = 0;
                    ObjectTracer.Enabled = true;
                    break;
                case CameraConfig.CameraMode.Focus:
                    cameraConfig.State = CameraConfig.CameraMode.Free;
                    Camera.main.orthographicSize = (cameraConfig.MaxZoomingSize + cameraConfig.MinZoomingSize) / 2;
                    ObjectTracer.Enabled = false;
                    break;
            }
            
        }
    }

    void LateUpdate()
    {
        switch (cameraConfig.State)
        {
            case CameraConfig.CameraMode.Focus:
                if (ObjectTracer.State == ObjectTracerController.TraceState.Idle)
                {
                    Camera.main.orthographicSize = cameraConfig.MaxZoomingSize;
                }
                break;
            case CameraConfig.CameraMode.Free:
                //Mouse Position Bottom Left = 0,0  /  Top Right = ScreenSize.x, ScreenSize.y
                if (Input.mousePosition.x < 1)
                {
                    //Restrict the camera to move outside of the map
                    if (transform.position.x - Camera.main.orthographicSize > cameraConfig.CameraBoundary.x)
                    {
                        offset.x -= cameraConfig.FreeMovementSpeed;
                    }
                }
                else if (Input.mousePosition.x >= Screen.width - 1)
                {
                    if (transform.position.x + Camera.main.orthographicSize < cameraConfig.CameraBoundary.x + cameraConfig.CameraBoundary.width)
                    {
                        offset.x += cameraConfig.FreeMovementSpeed;
                    }
                }
                if (Input.mousePosition.y < 1)
                {
                    if (transform.position.y + Camera.main.orthographicSize > cameraConfig.CameraBoundary.y - cameraConfig.CameraBoundary.height)
                    {
                        offset.y -= cameraConfig.FreeMovementSpeed;
                    }
                }
                else if (Input.mousePosition.y >= Screen.height - 1)
                {
                    if (transform.position.y - Camera.main.orthographicSize < cameraConfig.CameraBoundary.y)
                    {
                        offset.y += cameraConfig.FreeMovementSpeed;
                    }
                }
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * cameraConfig.MouseWheelZoomingSpeed, cameraConfig.MinZoomingSize, cameraConfig.MaxZoomingSize);
                }
                break;
        }
        if (ObjectTracer.Enabled)
        {
            if(ObjectTracer.State == ObjectTracerController.TraceState.Tracing){
                switch (ObjectTracer.Mode)
                {
                    case ObjectTracerController.TraceMode.ZoomIn:
                        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - ObjectTracer.ZoomingSpeed, ObjectTracer.MinSizeOnFocus, ObjectTracer.MaxSizeOnFocus);
                        break;
                    case ObjectTracerController.TraceMode.ZoomOut:
                        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + ObjectTracer.ZoomingSpeed, ObjectTracer.MinSizeOnFocus, ObjectTracer.MaxSizeOnFocus);
                        break;
                    default:
                        break;
                }
            }
            if(ObjectTracer.Traget != null){
                transform.position = ObjectTracer.Traget.transform.position + offset;
            }
        }
        else
        {
            if(player != null)
            {
                transform.position = player.transform.position + offset;
            }
        }
    }

    public void SetCameraState(CameraConfig.CameraMode state)
    {
        cameraConfig.State = state;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    

    public void CameraDelay(float time)
    {
        Invoke("ResetCamera", time);
    }

    private void ResetCamera()
    {
        ObjectTracer.SetFoucs(player, ObjectTracerController.TraceState.Idle);
    }
}