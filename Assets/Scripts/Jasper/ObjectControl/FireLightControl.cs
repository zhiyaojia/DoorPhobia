using UnityEngine;

public class FireLightControl : MonoBehaviour
{
    private Light myLight;
    private float lightIntensity;
    private float lightRange;

    private void Start()
    {
        myLight = GetComponent<Light>();
        lightIntensity = myLight.intensity;
        lightRange = myLight.range;
    }

    private void Update()
    {
        myLight.intensity = lightIntensity / 2f + Mathf.Lerp(lightIntensity - 0.1f, lightIntensity + 0.1f, Mathf.Cos(Time.time * 30));
        myLight.range = lightRange / 2f + Mathf.Lerp(lightRange - 0.1f, lightRange + 0.1f, Mathf.Cos(Time.time * 30));
    }
}
