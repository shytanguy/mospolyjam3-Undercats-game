using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteGUI : MonoBehaviour
{
    [SerializeField] private GameObject _note;

    [SerializeField] private TextMeshProUGUI _noteText;

    public static NoteGUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetText(string text)
    {
        _noteText.text = text;
    }
    public void TurnOn()
    {
        _note.SetActive(true);
        TimeController.StopTime(this);
    }
    public void TurnOff()
    {
        _note.SetActive(false);
        TimeController.ResumeTime(this);
    }
}
