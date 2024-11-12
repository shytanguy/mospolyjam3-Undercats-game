using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteScript : MonoBehaviour,IInteraction
{
    [SerializeField] private string _text;

    [SerializeField] private bool _destroyOnInteraction;
    [SerializeField] private AudioClip _sound;
    public void OnInteraction()
    {
        NoteGUI.instance.SetText(_text);
        NoteGUI.instance.TurnOn();
        AudioManager.audioManager.PlaySound(_sound);
        if (_destroyOnInteraction)
        {
            Destroy(gameObject);
        }
    }
}
