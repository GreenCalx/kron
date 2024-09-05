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

    void Start()
    {
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
