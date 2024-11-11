using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Cryptography;
using System;
using Unity.Mathematics;

public class CinemachineEffectsController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;

    public static CinemachineEffectsController instance;

    private bool zoomIn=false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    public void ShakeCamera(int amplitude, int frequency, float timeShake)
    {
        CinemachineBasicMultiChannelPerlin perlin = _cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
       
        StartCoroutine(ShakeCameraIE(amplitude, frequency, timeShake, perlin));
    }
  public void ZoomCamera(float time, float percentZoom,float timeInZoom)
    {
        CinemachineVirtualCamera camera = _cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(ZoomCameraForTime(time, percentZoom, camera,timeInZoom));
    }
    private IEnumerator ZoomCameraForTime(float timePerZoom, float percent, CinemachineVirtualCamera camera,float timeZoom)
    {
        if (zoomIn) yield break;
        zoomIn = true;
        float size = camera.m_Lens.OrthographicSize;
        float timer = 0;
        float targetSize = camera.m_Lens.OrthographicSize / percent;
        while (timer < timePerZoom)
        {
            camera.m_Lens.OrthographicSize = math.lerp(size,targetSize,timer/ timePerZoom);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSecondsRealtime(timeZoom);
        timer = 0;
        while (timer < timePerZoom)
        {
            camera.m_Lens.OrthographicSize = math.lerp(targetSize, size, timer / timePerZoom);
            timer += Time.deltaTime;
            yield return null;
        }
        zoomIn = false;
        camera.m_Lens.OrthographicSize = size;
    }
    private IEnumerator ShakeCameraIE(int amplitude, int frequency, float timeShake, CinemachineBasicMultiChannelPerlin perlin)
    {
        
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = amplitude;

      


        while (perlin.m_AmplitudeGain > 0 && perlin.m_FrequencyGain > 0)
        {
            perlin.m_AmplitudeGain -= amplitude / timeShake * 0.01f;
            perlin.m_FrequencyGain -= frequency / timeShake * 0.01f;
            yield return null;
        }
        if (perlin.m_FrequencyGain < 1 || perlin.m_FrequencyGain < 1)
        {
            perlin.m_AmplitudeGain = 0;
            perlin.m_FrequencyGain = 0;
        }
      
    }
}
