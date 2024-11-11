using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _timeExpanding;

    private Vector3 _size;
    private void Start()
    {
        _size = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowInSize());
    }
   private IEnumerator GrowInSize()
    {
        for (float i = 0; i <= _timeExpanding; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, _size, i);
            yield return null;
        }
    }
}
