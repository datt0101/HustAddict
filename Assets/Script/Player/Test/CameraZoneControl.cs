using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoneControl : MonoBehaviour
{

    [SerializeField]
    CinemachineVirtualCamera vCam;
    public float cameraSpeed;
    public static CameraZoneControl instance;
    private void Awake()
    {
        //vCam = FindObjectOfType<CinemachineVirtualCamera>();
        instance = this;
    }
    void LateUpdate()
    {
        vCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value += InputManager.instance.lookValue.y * cameraSpeed;
        vCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value += InputManager.instance.lookValue.x * -cameraSpeed;
    }
}
