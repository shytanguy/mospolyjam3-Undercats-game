using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] private Image _transition;

    public static Transition instance;
    [field: SerializeField] public float TimeTransitioning { get; private set; } = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

  
    }
    private void OnEnable()
    {
        TransitionIn();
    }
    public void TransitionIn()
    {
        StartCoroutine(TransitionInCoroutine(0));
    }

    public void TransitionOut()
    {
        StartCoroutine(TransitionInCoroutine(1));
    }

    private IEnumerator TransitionInCoroutine(float targetAlpha)
    {
        yield return new WaitForSeconds(0.5f);
        float alpha = _transition.color.a;
        for (float i = 0; i <= TimeTransitioning; i += Time.deltaTime)
        {
            alpha = math.lerp(alpha, targetAlpha, i);
            _transition.color =new Vector4(_transition.color.r,_transition.color.g,_transition.color.b, alpha);
            yield return null;
        }
        _transition.color= new Vector4(_transition.color.r, _transition.color.g, _transition.color.b, targetAlpha);
    }
}
