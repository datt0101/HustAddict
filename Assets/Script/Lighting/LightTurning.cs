using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTurning : MonoBehaviour
{
    [SerializeField] private GameObject lightPoint;
    [SerializeField] private GameObject lightArea;

    public void TurnOnLight()
    {
        lightPoint.SetActive(true);
        lightArea.SetActive(true);
    }

    public void TurnOffLight()
    {
        lightPoint.SetActive(false);
        lightArea.SetActive(false);
    }
    private void Reset()
    {
        LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        Transform transformChild1 = gameObject.transform.GetChild(4);
        lightPoint = transformChild1.gameObject;

        Transform transformChild2 = gameObject.transform.GetChild(3);
        lightArea = transformChild2.gameObject;


    }
}
