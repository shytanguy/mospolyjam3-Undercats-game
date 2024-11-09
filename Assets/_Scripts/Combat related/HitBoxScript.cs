using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    [SerializeField] private int _framesBeforeDamage=5;

    [SerializeField] private float _damage = 5f;

    [HideInInspector] public bool _canReflect { get; private set; } = true;

    private bool _Reflected = false;

    public event Action OnReflect;

    public event Action OnDamage;

   [SerializeField] private LayerMask _damageLayer;

    private bool _TriggerEntered = false;

    [SerializeField] private float _hitRadius;

    
    private IEnumerator DamageDelay()
    {
        for (int i = 1; i <= _framesBeforeDamage; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        _canReflect = false;

        Damage();
    }

    public void Reflect(FactionScript.Faction faction)
    {
        if (!_canReflect) return;
        else
        {
            _Reflected = true;
           
            OnReflect?.Invoke();
        }
    }
    private void Damage()
    {
        
      Collider2D[] hitColliders=  Physics2D.OverlapCircleAll(transform.position,_hitRadius,_damageLayer);

        foreach(var collider in hitColliders)
        {
            collider.GetComponent<HealthScript>().TakeDamage(_damage);
        }
        OnDamage?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if ((_damageLayer.value & (1 << collision.gameObject.layer)) > 0&&_TriggerEntered==false)
        {
            _TriggerEntered = true;
            StartCoroutine(DamageDelay());
        }
    }
}
