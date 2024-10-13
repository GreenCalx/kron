using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    public readonly string showBuildingLayer = "ShowBuildingTop";
    [Header("Manual Refs")]
    public GameCamera h_farCamera;
    public float camSmoothFactor = 0.02f;
    [Header("Auto Refs")]
    public GameCamera h_FPSCamera;
    [Header("Internals")]
    public GameCamera activeCamera;

    private int initCullingMaskFarCam;
    private bool initDone;

    private int lastCullingMaskForFarCam;

    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        initDone = false; 
        while (h_farCamera==null)
        {
            yield return null;
        }
        while (h_farCamera.selfCam==null)
        {
            yield return null;
        }

        initCullingMaskFarCam = h_farCamera.selfCam.cullingMask;
        lastCullingMaskForFarCam = initCullingMaskFarCam;
        activeCamera = h_farCamera;

        initDone = true;
    }

    void Update()
    {
        if (!initDone)
            return;

        if (Input.GetKeyUp("tab"))
        {
            if (Access.Player().freeze_inputs)
                return;
            changeCamera();
        }

        if (activeCamera==h_farCamera)
        {
            UpdateFarCam();
        }
    }
    public void CullAll(bool iState)
    {
        if (iState)
        {
            lastCullingMaskForFarCam = activeCamera.selfCam.cullingMask;
            activeCamera.selfCam.cullingMask = 0;
        } else {
            activeCamera.selfCam.cullingMask = lastCullingMaskForFarCam;
        }
    }

    void UpdateFarCam()
    {
        if (h_farCamera==null)
            return;
        if (h_farCamera.focus==null)
            return;

        Vector3 playerPosInScreen = activeCamera.selfCam.WorldToScreenPoint(activeCamera.focus.position);

        // Check X to width
        Vector3 tVec = Vector3.zero;

        if (playerPosInScreen.x < Screen.width/3)
        {
            tVec = new Vector3(-1f * camSmoothFactor, 0f, 0f);
            activeCamera.transform.Translate(tVec);
        }
        else if (playerPosInScreen.x > (Screen.width * 2/3))
        {
            tVec = new Vector3(camSmoothFactor, 0f, 0f);
            activeCamera.transform.Translate(tVec);
        }

        // Check Y to height
        if (playerPosInScreen.y < Screen.height/3)
        {
            tVec = new Vector3(0f, -1f * camSmoothFactor, 0f);
            activeCamera.transform.Translate(tVec);
        }
        else if (playerPosInScreen.y > (Screen.height * 2/3))
        {
            tVec = new Vector3(0f, camSmoothFactor, 0f);
            activeCamera.transform.Translate(tVec);
        }
    }

    public void ShowBuildingTop(bool iState)
    {
        int layerID = LayerMask.NameToLayer(showBuildingLayer);
        if (!iState)
            h_farCamera.selfCam.cullingMask &= ~(1 << layerID);
        else
            h_farCamera.selfCam.cullingMask = initCullingMaskFarCam;
    }

    void changeCamera()
    {
        if (activeCamera==h_farCamera)
        {
            if (h_FPSCamera!=null)
                h_FPSCamera.gameObject.SetActive(true);
            if (h_farCamera!=null)
                h_farCamera.gameObject.SetActive(false);
            activeCamera = h_FPSCamera;
            return;
        }

        if (h_FPSCamera!=null)
            h_FPSCamera.gameObject.SetActive(false);
        if (h_farCamera!=null)
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

    public void OnSceneLoaded()
    {
        RefreshCams();
        if (h_farCamera.focus==null)
            h_farCamera.focus = Access.Player().transform;
        activeCamera = h_farCamera;
        lastCullingMaskForFarCam = h_farCamera.selfCam.cullingMask;
    }

    public void CenterActiveCameraOn(Transform iTransform)
    {
        Vector3 tPosInScreen = activeCamera.selfCam.WorldToScreenPoint(iTransform.position);
        Vector3 wantedPos = activeCamera.selfCam.ScreenToWorldPoint(tPosInScreen);
        activeCamera.transform.position = wantedPos;
    }
}
