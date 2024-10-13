using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtil : MonoBehaviour
{
    [Header("Player Spawn")]
    public bool spawnPlayer;
    public GameObject prefab_player;
    public Transform PlayerSpawnPosition;
    public bool forcePlayerCamFocus;
    public bool setIntroAsDone = true;
    [Header("Spawn Manager")]
    public bool spawnManager;
    public GameObject prefab_manager;

    void Start()
    {
        TrySpawnManager();
        TrySpawnPlayer();
    }

    public void TrySpawnManager()
    {
        // scout if manager already exists by checking
        // a random component on it
        if (Access.CameraManager()!=null)
            return;
        if (spawnManager)
        {
            GameObject p = Instantiate(prefab_manager);
            p.gameObject.name = Constants.GO_MGR;
        }
    }

    public void TrySpawnPlayer()
    {
        if (Access.Player()!=null)
            return;

        if (spawnPlayer)
        {
            GameObject p = Instantiate(prefab_player);
            p.transform.parent = null;
            p.transform.position = PlayerSpawnPosition.position;
            p.gameObject.name = Constants.GO_PLAYER;

            if (forcePlayerCamFocus)
                StartCoroutine(ForceCamFocusOnPlayerCo());
            if (setIntroAsDone)
                PlayerPrefs.SetInt(Constants.PPKey_Intro, 1);
        }
    }

    IEnumerator ForceCamFocusOnPlayerCo()
    {
        
        while (Access.Player()==null) { yield return null; }
        while (Access.CameraManager()==null) { yield return null; }
        while (Access.CameraManager().h_farCamera==null) { yield return null; }
        Access.CameraManager().OnSceneLoaded();
    }
}
