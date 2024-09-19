using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public Transform h_DoorOpened;
    public Transform h_DoorClosed;
    public Transform h_DoorObject;
    public bool DoorInitState = false;
    public float SwitchTime=2f;
    public float SwitchStep = 0.1f;
    private Coroutine SelfCo;
    private bool p_opendoor;
    public bool OpenDoor
    {
        get { return p_opendoor; }
        set
        {
            if ((value!= p_opendoor)&&(SelfCo==null))
            {
                SelfCo=StartCoroutine(OpenCo(value));
            }
            p_opendoor = value;

        }
    }       
    void Start()
    {
        p_opendoor = DoorInitState;
    }
    IEnumerator OpenCo(bool iState)
    {
        Vector3 a = Vector3.zero;
        Vector3 b = Vector3.zero;
        float t = 0f;
        float i = 0f;
        bool NeedOpen,NeedClose;

        NeedOpen = (iState && (h_DoorObject.position != h_DoorOpened.position));
        NeedClose = (!iState && (h_DoorObject.position != h_DoorClosed.position));
        if (NeedOpen)
        {
            a = h_DoorClosed.position;
            b = h_DoorOpened.position;
            i = 0;
            while (NeedOpen)
            {
                t = (SwitchStep * (float)i) / SwitchTime;
                i++;
                h_DoorObject.position = Vector3.Lerp(a, b, t);
                NeedOpen = (iState && (h_DoorObject.position != h_DoorOpened.position));
                yield return new WaitForSeconds(SwitchStep);
            }
        }
        if (NeedClose)
        {
            b = h_DoorClosed.position;
            a = h_DoorOpened.position;
            i = 0;
            while (NeedClose)
            {
                t = (SwitchStep * (float)i) / SwitchTime;
                i++;
                h_DoorObject.position = Vector3.Lerp(a, b, t);
                NeedClose = (!iState && (h_DoorObject.position != h_DoorClosed.position));
                yield return new WaitForSeconds(SwitchStep);
            }
        }
    }

}
