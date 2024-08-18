using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light realtimelight;
    public List<GameObject> spotLights = new List<GameObject>();
    bool tempFlag = true;

    public void SetLightTime()
    {
        if (TimeManager.Hour <= 12)
            realtimelight.intensity = (float) ((float)TimeManager.Hour) / 10;
        else
            realtimelight.intensity = ((float)TimeManager.Hour) / 10 - ((float)TimeManager.Hour - 12) / 5;
    }
    private void Update()
    {
        SetLightTime();
        if(realtimelight.intensity > 0.7)
        {
            if(tempFlag)
            {
                tempFlag = false;
                foreach (GameObject light in spotLights)
                {
                    light.SetActive(false);
                }
            }
            
        }
        else
        {
            if(!tempFlag)
            {
                tempFlag = true;
                foreach (GameObject light in spotLights)
                {
                    light.SetActive(true);
                }
            }
            
        }
    }
}
