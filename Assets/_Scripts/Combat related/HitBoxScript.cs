using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{

    public enum DamageType
    {
        onEnter,
        onStay
    }
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

    [SerializeField] private DamageType _type;

    private float _timer;

    [SerializeField] private float _secondsBetweenDamage=0.5f;
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
            OnReflect?.Invoke(faction);
            return true;
          
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _hitRadius);
        if (_canReflect == false)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(transform.position, _hitRadius);
        }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_type==DamageType.onStay)
        if ((_damageLayer.value & (1 << collision.gameObject.layer)) > 0 && _TriggerEntered == false)
        { if (Time.time - _timer >= _secondsBetweenDamage)
                {
                    _timer = Time.time;
                    Damage();
                }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_type == DamageType.onEnter)
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
