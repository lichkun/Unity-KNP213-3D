using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField]
    private Material nightSkyBox;
    [SerializeField]
    private Material LightSkyBox;
    private List<Light> nightLights;
    private List<Light> dayLights;
    private bool isNight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nightLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("NightLight"))
        {
            nightLights.Add(g.GetComponent<Light>());
        }
        dayLights = new List<Light>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("DayLight"))
        {
            dayLights.Add(g.GetComponent<Light>());
        }
        isNight = nightLights[0].isActiveAndEnabled;
        RenderSettings.skybox = nightSkyBox;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            isNight = !isNight;
            foreach (Light nightLight in nightLights)
            {
                nightLight.enabled = isNight;
            }
            foreach (Light dayLight in dayLights)
            {
                dayLight.enabled = !isNight;
            }
            if (isNight)
            {
                RenderSettings.skybox = nightSkyBox;
            }
            else
            {
                RenderSettings.skybox = LightSkyBox;
            }
        }
    }
}
