using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class TravelToLevel : MonoBehaviour
{
    public Transform h_SpawnPoint;
    public string targetLevel = string.Empty;
    public float TimeToTravel = 1f;
    private Coroutine WaitToTeleportCo;
    public bool teleportOngoing;
    private float elapsedTime = 0f;
    public bool NeedExitAfterTeleport = false;
    void Start()
    {
        WaitToTeleportCo = null;
        teleportOngoing = false;
        StartCoroutine(LookForPlayerRepositioning());
    }

    IEnumerator LookForPlayerRepositioning()
    {
        while (Access.Player()==null)
        {
            yield return null;
        }
        PlayerController pc = Access.Player();
        if (pc.previousScene==string.Empty)
            yield break;
        if (pc.previousScene==targetLevel)
        {
            pc.transform.position = h_SpawnPoint.position;
            pc.transform.rotation = h_SpawnPoint.rotation;
            NeedExitAfterTeleport = true;

            yield return null;

            Access.CameraManager().CenterActiveCameraOn(h_SpawnPoint);
        }

    }

    void OnTriggerStay(Collider iCollider)
    {
        if (teleportOngoing)
            return;

        if (NeedExitAfterTeleport)
            return;

        if (Utils.ColliderIsPlayer(iCollider))
        {
            WaitToTeleportCo = StartCoroutine(TeleportPlayerCo());
        }
    }

    void OnTriggerExit(Collider iCollider)
    {
        if (!Utils.ColliderIsPlayer(iCollider))
            return;
        
        if (NeedExitAfterTeleport)
            NeedExitAfterTeleport = false;

        if (teleportOngoing)
        {
            StopCoroutine(WaitToTeleportCo);

            WaitToTeleportCo = null;
            teleportOngoing = false;
        }
    }

    IEnumerator TeleportPlayerCo()
    {
        teleportOngoing = true;
        elapsedTime = 0f;
        while(teleportOngoing)
        {
            elapsedTime += 0.1f;
            if (elapsedTime>=TimeToTravel)
                break;
            yield return new WaitForSeconds(0.1f);
        }
    
        Access.SceneLoader().LoadLevel(targetLevel);
    }
}
