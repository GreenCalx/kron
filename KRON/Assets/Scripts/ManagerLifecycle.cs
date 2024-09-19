using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLifecycle : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int n_manager_handler = FindObjectsOfType<ManagerLifecycle>().Length;
        if (n_manager_handler > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
