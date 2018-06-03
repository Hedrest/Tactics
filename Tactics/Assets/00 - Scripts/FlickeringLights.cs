using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Light))]
public class FlickeringLights : MonoBehaviour
{

    private Light light;

    public float minOnTime = 0.2f;
    public float maxOnTime = 1.1f;

    public float minOffTime = 0.35f;
    public float maxOffTime = 2.25f;

    public float minIntensity = 0.1f;
    public float maxIntensity = 0.9f;

    public bool flickerOnStart = true;
    // Use this for initialization
    void Start()
    {
        light = GetComponent<Light>();
        light.enabled = false;
        if (flickerOnStart)
        {
            StartFlicker();
        }
    }

    public float PickRandomIntensity()
    {
        return Random.Range(minIntensity, maxIntensity);
    }

    public void StartFlicker()
    {
        StartCoroutine(Flicker());
    }

    public void StopFlicker()
    {
        StopAllCoroutines();
    }

    IEnumerator Flicker()
    {
        yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
        light.intensity = PickRandomIntensity();
        light.enabled = true;
        yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
        light.enabled = false;
        StartCoroutine(Flicker());
    }
}
