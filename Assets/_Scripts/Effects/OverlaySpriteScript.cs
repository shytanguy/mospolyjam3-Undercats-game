using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlaySpriteScript : MonoBehaviour
{
    private SpriteRenderer _overlay;

    [SerializeField] private float _fadeDuration=0.5f;
    [SerializeField] private SpriteRenderer _originalSprite;
    private SpriteMask _mask;
    [SerializeField] private bool _overlayOriginalSprite = false;
    private void Awake()
    {
        _overlay = GetComponent<SpriteRenderer>();
        if (!_overlayOriginalSprite)
        _mask = GetComponent<SpriteMask>();
    }

    public void OverlayColorWhite()
    {
      
        StopAllCoroutines();
        _overlay.color = Color.white;
        if (_overlayOriginalSprite)
        {
            StartCoroutine(ReturnToWhite());
        }
        else
        StartCoroutine(MakeTransparent());
    }
    public void OverlayColorRed()
    {
        StopAllCoroutines();
        _overlay.color = Color.red;
        if (_overlayOriginalSprite)
        {
            StartCoroutine(ReturnToWhite());
        }
        else
            StartCoroutine(MakeTransparent());
    }
    public void OverlayColorGreen()
    {
        StopAllCoroutines();

        _overlay.color = Color.green;
        if (_overlayOriginalSprite)
        {
            StartCoroutine(ReturnToWhite());
        }
        else
            StartCoroutine(MakeTransparent());
    }
    private void Update()
    {
        if (!_overlayOriginalSprite)
        _mask.sprite = _originalSprite.sprite;
    }
    private IEnumerator ReturnToWhite()
    {
        Color color = _overlay.color;

   
        for (float t = 0; t < _fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / _fadeDuration;
         
            _overlay.color = Vector4.Lerp(color, Vector4.one, normalizedTime);
            yield return null;
        }

        
        _overlay.color = Vector4.one;
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
