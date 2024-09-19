using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public readonly string showBuildingLayer = "ShowBuildingTop";
    [Header("Manual Refs")]
    public Camera h_farCamera;
    [Header("Auto Refs")]
    public Camera h_FPSCamera;
    [Header("Internals")]
    public Camera activeCamera;

    private int initCullingMaskFarCam;
    private bool initDone;

    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        initDone = false; 
        initCullingMaskFarCam = h_farCamera.cullingMask;
        activeCamera = null;
        while(Access.Player()==null)
        {
            yield return null;
        }
        RefreshCams();
        changeCamera();
        initDone = true;
    }

    void Update()
    {
        if (!initDone)
            return;

        if (Input.GetKeyUp("space"))
        {
            if (Access.Player().freeze_inputs)
                return;
            changeCamera();
        }
    }

    public void ShowBuildingTop(bool iState)
    {
        int layerID = LayerMask.NameToLayer(showBuildingLayer);
        if (!iState)
            h_farCamera.cullingMask &= ~(1 << layerID);
        else
            h_farCamera.cullingMask = initCullingMaskFarCam;
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

    private void RefreshCams()
    {
        if (h_FPSCamera==null)
        {
            PlayerController pc = Access.Player();
            if (null==pc)
                return;
            h_FPSCamera = Access.Player().FPSCamera;
        }
    }
}
