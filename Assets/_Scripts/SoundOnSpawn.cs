using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnSpawn : MonoBehaviour
{
    [SerializeField] private AudioClip _soundOnSpawn;

    private void Start()
    {
        AudioManager.audioManager.PlaySound(_soundOnSpawn);
    }
}
