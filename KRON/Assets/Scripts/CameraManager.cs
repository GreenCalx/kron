using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Mandatory Refs")]
    public Camera h_farCamera;
    public Camera h_FPSCamera;
    [Header("Internals")]
    public Camera activeCamera;

    private int initCullingMaskFarCam;

    void Start()
    {
        //initCullingMaskFarCam = h_farCamera.cullingMask;
        activeCamera = null;
        changeCamera();
    }
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            changeCamera();
        }
    }

    public void ShowBuildingTop(bool iState)
    {
        Debug.Log("Before : " + h_farCamera.cullingMask);
        var bitVal = iState?1:0;
        h_farCamera.cullingMask |= 1 << 5;
        Debug.Log("after : " + h_farCamera.cullingMask);
    }

    void changeCamera()
    {
        if (activeCamera==h_farCamera)
        {
            h_FPSCamera.gameObject.SetActive(true);
            h_farCamera.gameObject.SetActive(false);
            activeCamera = h_FPSCamera;
            return;
        }

        h_FPSCamera.gameObject.SetActive(false);
        h_farCamera.gameObject.SetActive(true);
        activeCamera = h_farCamera;
    }
}
