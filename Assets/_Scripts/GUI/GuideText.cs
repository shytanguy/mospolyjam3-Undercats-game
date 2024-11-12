using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class GuideText : MonoBehaviour
{

    private TextMeshProUGUI _guideText;
    private void Awake()
    {


        _guideText = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        _guideText.text = text;
    }
    
    public void ClearText()
    {
        _guideText.text = "";
    }
}
