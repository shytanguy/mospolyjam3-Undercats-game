using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAbstract : MonoBehaviour
{
    protected Rigidbody2D _projectileRigidBody;

    protected FactionScript _faction;

   protected HitBoxScript _hitbox;

    [SerializeField] protected float _Speed = 3f;

  
    private void Awake()
    {
        _projectileRigidBody = GetComponent<Rigidbody2D>();
        _faction = GetComponent<FactionScript>();
        _hitbox = GetComponent<HitBoxScript>();
    }
    private void OnEnable()
    {
        _hitbox.OnReflect += Reflect;
        _hitbox.OnDamage += OnHit;
    }
    private void OnDisable()
    {
        _hitbox.OnReflect -= Reflect;
        _hitbox.OnDamage -= OnHit;
    }
    private void Reflect(FactionScript.Faction faction)
    {
        _faction.ChangeFaction(faction);
    }
    private void OnHit()
    {
        Destroy(gameObject);
    }
}
