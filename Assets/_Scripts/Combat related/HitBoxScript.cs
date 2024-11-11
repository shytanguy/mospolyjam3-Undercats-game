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

   [SerializeField] private LayerMask _PlayerDamageLayer;

    [SerializeField] private LayerMask _enemyLayers;

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
            _TriggerEntered = false;
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
        LayerMask layermask = _PlayerDamageLayer | _enemyLayers;
      Collider2D[] hitColliders=  Physics2D.OverlapCircleAll(transform.position,_hitRadius,layermask);
        Debug.Log(_faction.userFaction);
        foreach (var collider in hitColliders)
        {
          
            if (collider.GetComponent<FactionScript>().userFaction == _faction.userFaction) continue;
            Debug.Log("hitting ");
            collider.GetComponent<HealthScript>().TakeDamage(_damage);
            collider.GetComponent<Rigidbody2D>().AddForce(_knockBack * (collider.transform.position - transform.position).normalized, ForceMode2D.Impulse);
        }
        if (hitColliders!=null)
        OnDamage?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_type == DamageType.onStay)
        {
            if (((_faction.userFaction == FactionScript.Faction.enemy) && (_PlayerDamageLayer.value & (1 << collision.gameObject.layer)) > 0 ))
            {
                if (Time.time - _timer >= _secondsBetweenDamage)
                {
                    _timer = Time.time;
                    Damage();
                }
            }
            else if (((_faction.userFaction == FactionScript.Faction.player) && (_enemyLayers.value & (1 << collision.gameObject.layer)) > 0 ))
            {
                if (Time.time - _timer >= _secondsBetweenDamage)
                {
                    _timer = Time.time;
                    Damage();
                }
            }
            else if (((_faction.userFaction == FactionScript.Faction.ultimate) && (((_enemyLayers.value & (1 << collision.gameObject.layer)) > 0)|| (_PlayerDamageLayer.value & (1 << collision.gameObject.layer)) > 0)))
            {
                if (Time.time - _timer >= _secondsBetweenDamage)
                {
                    _timer = Time.time;
                    Damage();
                }
            }
           
        }
    }
    private void TriggerEnter()
    {
        _TriggerEntered = true;
        StopAllCoroutines();
        if (_delayDamage)
        {
            StartCoroutine(DamageDelay());
        }
        else
        {
            Damage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_type == DamageType.onEnter)
        {
            if (((_faction.userFaction==FactionScript.Faction.enemy) &&(_PlayerDamageLayer.value & (1 << collision.gameObject.layer)) > 0 && _TriggerEntered == false))
            {
                TriggerEnter();
            }
            else if(((_faction.userFaction == FactionScript.Faction.player) && (_enemyLayers.value & (1 << collision.gameObject.layer)) > 0 && _TriggerEntered == false))
            {

                Damage();
            }
            
        }
    }
}
