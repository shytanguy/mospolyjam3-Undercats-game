using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    private TextMeshProUGUI _errorText;

    [SerializeField]  private float _timeAnimating;

    [SerializeField] private ModuleControllerAbstract[] modules;
    private void Awake()
    {
        _errorText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        foreach(var module in modules)
        {
            module.OnActivationMessage += SetText;
        }
    }
    private void OnDisable()
    {
        foreach (var module in modules)
        {
            module.OnActivationMessage -= SetText;
        }
    }
    public void SetText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(TextAnimation(text));
    }

    private IEnumerator TextAnimation(string text)
    {
        float elapsedTime = 0f;
        int i = 1;
        while (elapsedTime < _timeAnimating)
        {
           
            _errorText.text = ShuffleString(text).Remove(i,text.Length-i);
            if (i < text.Length)
            i++;
            elapsedTime += 0.03f;

          
            yield return new WaitForSeconds(0.03f);
        }

       
        _errorText.text = text;
        StartCoroutine(TextAnimationOff());
    }
    private IEnumerator TextAnimationOff()
    {
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0f;

        while (elapsedTime < _timeAnimating)
        {
            if (_errorText.text.Length>0)
            _errorText.text= _errorText.text.Remove(_errorText.text.Length-1);
            _errorText.text = ShuffleString(_errorText.text);


            elapsedTime += 0.03f;


            yield return new WaitForSeconds(0.03f);
        }


        _errorText.text = "";
    }


    private string ShuffleString(string input)
    {
        System.Random rng = new System.Random();
        char[] array = input.ToCharArray();
        int n = array.Length;

  
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
    
            char temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return new string(array);
    }
}
