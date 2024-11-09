using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    private TextMeshProUGUI _errorText;

    [SerializeField]  private float _timeAnimating;


    private void Awake()
    {
        _errorText = GetComponent<TextMeshProUGUI>();
    }
    private void SetText(string text)
    {
        StartCoroutine(TextAnimation(text));
    }

    private IEnumerator TextAnimation(string text)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _timeAnimating)
        {
            
            _errorText.text = ShuffleString(text);

            
            elapsedTime += Time.deltaTime;

          
            yield return null;
        }

       
        _errorText.text = text;
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
