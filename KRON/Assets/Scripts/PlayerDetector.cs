using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    public UnityEvent callbackOnTriggerEnter;
    public UnityEvent callbackOnTriggerExit;

    private bool triggDone = false;

    void Start()
    {
        triggDone = false;
    }

    void OnTriggerEnter(Collider iCollider)
    {
        if (triggDone)
            return;

        if (iCollider.gameObject.GetComponent<PlayerController>())
        {
            callbackOnTriggerEnter.Invoke();
            triggDone = true;
        }
    }

    void OnTriggerExit(Collider iCollider)
    {
        if (!triggDone)
            return;

        if (iCollider.gameObject.GetComponent<PlayerController>())
        {
            callbackOnTriggerExit.Invoke();
            triggDone = false;
        }
    }
}
