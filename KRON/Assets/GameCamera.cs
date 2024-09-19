using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCamera : MonoBehaviour
{
    public enum CAM_TYPE {NONE, FAR, FPS};

    public CAM_TYPE camType;
    // Start is called before the first frame update
    void Start()
    {
        if (camType==CAM_TYPE.FAR)
            Access.CameraManager().h_farCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
