using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour,IInteraction
{
    [SerializeField] private string _text;

    public void OnInteraction()
    {
        NoteGUI.instance.SetText(_text);
    }
}
