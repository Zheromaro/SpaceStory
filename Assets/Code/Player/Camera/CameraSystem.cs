using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [HideInInspector] public float shakeTimer;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private CamerasTrigger[] triggers;


    private int EventTimes = 1;

    private void OnEnable()
    {
        foreach (var cam in cameras)
        {
            CameraSwitcher.Register(cam);
        }
    }

    private void Start()
    {
        CameraSwitcher.ActiveCamera = cameras[0];

        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].playerIsHere += CameraSystem_playerIsHere;
        }
    }

    private void OnDisable()
    {
        foreach (var cam in cameras)
        {
            CameraSwitcher.Unregister(cam);
        }
    }

    void Update()
    {
        cinemachineBasicMultiChannelPerlin = CameraSwitcher.ActiveCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
        
    }

    private void CameraSystem_playerIsHere(object sender, System.EventArgs e)
    {
        CameraSwitcher.SwitchCamera(cameras[EventTimes]);
        Debug.Log(CameraSwitcher.ActiveCamera);
        PlayerMovement.theTrueSpeed = 0.1f;
        EventTimes++;
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
}
