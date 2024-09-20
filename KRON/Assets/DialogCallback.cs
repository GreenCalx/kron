using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogCallback : MonoBehaviour
{
    public TalkativeNPC dialogTarget;
    public bool TriggerOnlyOnce = true;
    public int DialogIDToTriggerCB;
    public UnityEvent callback;
    private bool HasBeenTriggered = false;
    void Start()
    {
        if (dialogTarget==null)
        {
            Debug.LogError("Missing dialogTarget on dialogCallback");
            Destroy(this);
            return;
        }
        HasBeenTriggered = false;
    }

    void Update()
    {
        if (HasBeenTriggered && TriggerOnlyOnce)
            return;
        if (!dialogTarget.playerIsTalking)
            return;
        if (dialogTarget.dialog_ids[dialogTarget.currDialogIndex]==DialogIDToTriggerCB)
        {
            callback.Invoke();
            HasBeenTriggered = true;
        }
    }
}
