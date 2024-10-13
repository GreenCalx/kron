using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCamera : MonoBehaviour
{
    public enum CAM_TYPE {NONE, FAR, FPS};

    public CAM_TYPE camType;
    public Camera selfCam;
    public Transform focus;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(initCo());
    }

    IEnumerator initCo()
    {
        while ( Access.CameraManager()==null)
        {
            yield return null;
        }
        if (camType==CAM_TYPE.FAR)
            Access.CameraManager().h_farCamera = this;
        else if (camType==CAM_TYPE.FPS)
            Access.CameraManager().h_FPSCamera = this;
        
        selfCam = GetComponent<Camera>();
    }

}
