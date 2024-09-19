using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BalenosSpawn : MonoBehaviour
{
    public float throw_force = 12f;
    public readonly string MouthOpenClip = "MouthOpened";
    public GameObject prefab_player;

    public Transform PlayerSpawnPosition;
    public Animator self_animator;
    public bool IntroAlreadyDone = false;
    void Start()
    {
        IntroAlreadyDone = PlayerPrefs.GetInt(Constants.PPKey_Intro)==1;
        if (IntroAlreadyDone)
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AnimatorStateInfo currClip = self_animator.GetCurrentAnimatorStateInfo(0);
        if ((currClip.IsName(MouthOpenClip)))
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    IEnumerator SpawnPlayer()
    {
        if (IntroAlreadyDone)
            yield break;

        GameObject p = Instantiate(prefab_player);
        p.transform.parent = null;
        p.transform.position = PlayerSpawnPosition.position;
        p.gameObject.name = Constants.GO_PLAYER;

        yield return new WaitForFixedUpdate();
        
        Vector3 lookPosXZ = new Vector3(transform.position.x, p.transform.position.y, transform.position.z);
        p.transform.LookAt(lookPosXZ);

        // if we want to be rotated towards the exit of the pier
        //Quaternion rot = Quaternion.LookRotation(lookPosXZ);
        
        PlayerController pc = p.GetComponentInChildren<PlayerController>();
        if (!!pc)
        {
            pc.PlayThrownOutOfBalenos(transform, throw_force);
        }
        
        PlayerPrefs.SetInt(Constants.PPKey_Intro, 1);
        Destroy(this);
    }
}
