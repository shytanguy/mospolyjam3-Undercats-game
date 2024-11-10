using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour,IInteraction
{
    [SerializeField] private string _text;

    [SerializeField] private bool _destroyOnInteraction;
    public void OnInteraction()
    {
        NoteGUI.instance.SetText(_text);
        NoteGUI.instance.TurnOn();
        if (_destroyOnInteraction)
        {
            Destroy(gameObject);
        }
    }
}
