using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlaySpriteScript : MonoBehaviour
{
    private SpriteRenderer _overlay;

    [SerializeField] private float _fadeDuration=0.5f;
    private void Awake()
    {
        _overlay = GetComponent<SpriteRenderer>();
    }

    public void OverlayColorWhite()
    {
        StopCoroutine(MakeTransparent());

        _overlay.color = Color.white;

        StartCoroutine(MakeTransparent());
    }
    public void OverlayColorRed()
    {
        StopCoroutine(MakeTransparent());

        _overlay.color = Color.red;

        StartCoroutine(MakeTransparent());
    }
    public void OverlayColorGreen()
    {
        StopCoroutine(MakeTransparent());

        _overlay.color = Color.green;

        StartCoroutine(MakeTransparent());
    }
    private IEnumerator MakeTransparent()
    {

        Color color = _overlay.color;

        float startAlpha = color.a;

        for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / _fadeDuration;
            color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            _overlay.color = color;
            yield return null;
        }

        color.a = 0;
        _overlay.color = color;
    }
}
