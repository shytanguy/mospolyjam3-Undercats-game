using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Security.Cryptography;

public class CinemachineShakeController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;

    public static CinemachineShakeController instance;
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
