using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public CameraManager cameraManager;

    public List<MeshRenderer> ToCullOnEnter;
    public bool isShown = true;
    public bool playerIsIn = false;

    void Start()
    {
        isShown = true;
        playerIsIn = false;
        if (cameraManager==null)
        {
            cameraManager = Access.CameraManager();
        }
    }


    public void CullToggle()
    {
        isShown = !isShown;
        // foreach(MeshRenderer mr in ToCullOnEnter)
        // {
        //     mr.gameObject.SetActive(isShown);
        // }
        cameraManager.ShowBuildingTop(isShown);
    }

    public void PlayerIsInToggle()
    {
        playerIsIn = !playerIsIn;
    }
}
