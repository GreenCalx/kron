using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public List<MeshRenderer> ToCullOnEnter;
    public bool isShown = true;
    public bool playerIsIn = false;

    void Start()
    {
        isShown = true;
        playerIsIn = false;
    }


    public void CullToggle()
    {
        // if (!isShown && !playerIsIn)
        // {
        //     isShown = true;
        // } else if (playerIsIn && !isShown) {
        //     return;
        // } else {
        //     isShown = !isShown;
        // }

        isShown = !isShown;
        foreach(MeshRenderer mr in ToCullOnEnter)
        {
            mr.gameObject.SetActive(isShown);
        }
    }

    public void PlayerIsInToggle()
    {
        playerIsIn = !playerIsIn;
    }
}
