using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlotText : MonoBehaviour
{
  private TextMeshProUGUI _PlotText;
    public static PlotText instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }

        _PlotText = GetComponent<TextMeshProUGUI>();
    }
    public void SetText(float characterSpeed, float timeStaying, string text)
    {
        StartCoroutine(TextAnimation(characterSpeed, timeStaying, text));
    }

    private IEnumerator TextAnimation(float characterSpeed, float timeStaying, string text)
    {

        _PlotText.enableAutoSizing = true;
        _PlotText.text = text;

        _PlotText.ForceMeshUpdate(true, true);
        _PlotText.enableAutoSizing = false;
        _PlotText.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            _PlotText.text += text[i];
            yield return new WaitForSeconds(characterSpeed);
        }
        _PlotText.text = text;
        yield return new WaitForSeconds(timeStaying);
        _PlotText.text = "";
    }


}
