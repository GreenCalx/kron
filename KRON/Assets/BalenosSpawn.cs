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
        GameObject p = Instantiate(prefab_player);
        p.transform.parent = null;
        p.transform.position = PlayerSpawnPosition.position;

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
            
        Destroy(this);
    }
}
