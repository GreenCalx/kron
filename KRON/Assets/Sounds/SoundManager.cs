using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BGM_Source;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlayBGM()
    {
        BGM_Source.Play();
    }

    public void StopBGM()
    {
        BGM_Source.Stop();
    }

    public void SetBGM(AudioClip iClip)
    {
        BGM_Source.clip = iClip;
    }
}
