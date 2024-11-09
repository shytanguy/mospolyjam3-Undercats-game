using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollider : MonoBehaviour
{
    public UnityEvent TriggerEvent;

    [SerializeField] private LayerMask _triggerLayerMask;

   [SerializeField] private bool _DestroyOnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_triggerLayerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            TriggerEvent?.Invoke();
            if (_DestroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
