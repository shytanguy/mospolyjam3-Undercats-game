using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    [SerializeField] private float _SecondsBeforeDamage=0.5f;

    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _knockBack = 5f;

    [HideInInspector] public bool _canReflect { get; private set; } = true;

  [HideInInspector]  public bool Reflected { get; private set; } = false;

    public event Action<FactionScript.Faction> OnReflect;

    public event Action OnDamage;

   [SerializeField] private LayerMask _damageLayer;

    private bool _TriggerEntered = false;

    [SerializeField] private float _hitRadius;

    private FactionScript _faction;

    [SerializeField] private bool _delayDamage=true;
    private void Awake()
    {
        _faction = GetComponent<FactionScript>();
    }
    private void OnEnable()
    {
        OnReflect += ChangeFaction;
    }
    private void OnDisable()
    {
        OnReflect -= ChangeFaction;
    }
    private IEnumerator DamageDelay()
    {
        
        for (float i = 0; i <= _SecondsBeforeDamage; i+=Time.deltaTime)
        {
            yield return null;
        }
        if (Reflected) yield break;
        _canReflect = false;

        Damage();
    }
   
    private void ChangeFaction(FactionScript.Faction faction)
    {
        _faction.ChangeFaction(faction);
    }
    public bool Reflect(FactionScript.Faction faction)
    {
        if (!_canReflect) return false;
        else
        {
            Reflected = true;
            return true;
            OnReflect?.Invoke(faction);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _hitRadius);
    }
    private void Damage()
    {
        
      Collider2D[] hitColliders=  Physics2D.OverlapCircleAll(transform.position,_hitRadius,_damageLayer);

        foreach(var collider in hitColliders)
        {
            if (collider.GetComponent<FactionScript>().userFaction == _faction.userFaction) continue;
            collider.GetComponent<HealthScript>().TakeDamage(_damage);
            collider.GetComponent<Rigidbody2D>().AddForce(_knockBack * (collider.transform.position - transform.position).normalized, ForceMode2D.Impulse);
        }
        OnDamage?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_damageLayer.value & (1 << collision.gameObject.layer)) > 0 && _TriggerEntered == false)
        {
            _TriggerEntered = true;
            if (_delayDamage)
            {
                StartCoroutine(DamageDelay());
            }
            else
            {
                Damage();
            }
        }
    }
}
