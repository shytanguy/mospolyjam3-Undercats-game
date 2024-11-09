using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DestroyObjectOnTime : MonoBehaviour
{
    [SerializeField] private float _TimeBeforeDeleted = 20f;

    [SerializeField] private float _duration=1;

    [SerializeField] private bool _destroyInstantly = false;
    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(_TimeBeforeDeleted);
        if (_destroyInstantly)
        {
            Destroy(gameObject);
            yield break;
        }
        Vector3 startingScale = transform.localScale;
        for (float t = 0; t <_duration; t += Time.deltaTime)
        {
            float normalizedTime = t / _duration;
            transform.localScale = Vector3.Lerp(startingScale, Vector3.zero, normalizedTime);
            
            yield return null;
        }
        Destroy(gameObject);
    }
}
