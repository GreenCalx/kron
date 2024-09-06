using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightAnimation : MonoBehaviour
{
    public bool animate = true;
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float cycleDuration = 3f;

    public Light selfLight;

    // Start is called before the first frame update
    void Start()
    {
        if (selfLight==null)
            selfLight = GetComponent<Light>();
        StartCoroutine(AnimCo());
    }

    IEnumerator AnimCo()
    {
        
        bool intensityGoesUp = true;
        selfLight.intensity = minIntensity;
        
        float startTime = Time.time;
        while(animate)
        {
            float t = (Time.time - startTime) / cycleDuration;

            if (intensityGoesUp)
                selfLight.intensity = Mathf.SmoothStep(minIntensity, maxIntensity, t);
            else
                selfLight.intensity = Mathf.SmoothStep(maxIntensity, minIntensity, t);
                
            if (selfLight.intensity>=maxIntensity)
            { intensityGoesUp = false; startTime = Time.time; }
            else if (selfLight.intensity<=minIntensity)
            { intensityGoesUp = true; startTime = Time.time; }
            
            yield return null;
        }
    }
}
