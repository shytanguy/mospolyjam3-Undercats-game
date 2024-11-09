using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public  class PlayerAttackState : PlayerAbstractState
{
    [SerializeField] protected float _TimeBeforeSwitchState;

    [SerializeField] protected float _TimeBeforeInput;

    [SerializeField] protected float _HitBoxSpawnDelay;
    
    protected bool _canInput;

    [SerializeField] private float _damage;

    [SerializeField] protected Vector2 _hitBoxDimensions;

    [SerializeField] private Vector2 _hitBoxOffset;

    [SerializeField] private bool _drawGizmos;

    [SerializeField] private LayerMask _hitBoxLayerMask;
    public override void EnterState()
    {
        _canInput = false;

        StartCoroutine(HitBoxSpawnDelay());

        StartCoroutine(InputDelay());

        StartCoroutine(SwitchStateDelay());

        _componentsManager.playerInput.actions["Attack"].performed += OnAttackInput;
    }
    public override void ExitState()
    {
        _componentsManager.playerInput.actions["Attack"].performed -= OnAttackInput;
    }
    private void OnAttackInput(InputAction.CallbackContext context)
    {
        if (!_canInput) return;

        Vector2 direction = _componentsManager.playerInput.actions["Move"].ReadValue<Vector2>();

        switch (direction.y)
        {
            case 0: _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackMiddle); break;
            case (> 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackTop); break;
            case (< 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackDown); break;
        }
    }
    public override void UpdateState()
    {
        if (!_canInput) return;

        CheckWalkState();
    }
    private void CheckWalkState()
    {
        if (_componentsManager.playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero)
        {
            _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.walk);
        }
    }
    private void Attack()
    {
        Vector3 offset;
        if (transform.eulerAngles.y == 0)
        {
             offset = _hitBoxOffset;
        }
        else
        {
            offset = new Vector3(-_hitBoxOffset.x, _hitBoxOffset.y);
        }
     Collider2D[] Hit=   Physics2D.OverlapBoxAll(transform.position + offset, _hitBoxDimensions, 0, _hitBoxLayerMask);
        foreach(var collider in Hit)
        {
            HitBoxScript hitbox;
            if(collider.TryGetComponent<HitBoxScript>(out hitbox))
            {
                hitbox.Reflect(GetComponent<FactionScript>().userFaction);
            }
            HealthScript hp;
            if (collider.TryGetComponent<HealthScript>(out hp))
            {
                hp.TakeDamage(_damage);
            }
            
        }
      
    }

    private void OnDrawGizmosSelected()
    {
        if (!_drawGizmos) return;

        Gizmos.color = Color.red;
        Vector3 offset;
        if (transform.eulerAngles.y == 0)
        {
            offset = _hitBoxOffset;
        }
        else
        {
            offset = new Vector3(-_hitBoxOffset.x, _hitBoxOffset.y);
        }
      
        Gizmos.DrawCube(transform.position + offset, _hitBoxDimensions);
    }
    protected IEnumerator HitBoxSpawnDelay()
    {
        yield return new WaitForSeconds(_HitBoxSpawnDelay);

        if (_StatesManager.GetStateKey() == this.stateKey)
        {
            Attack();
        }
    }
    protected IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(_TimeBeforeInput);

        _canInput = true;

    }
    protected IEnumerator SwitchStateDelay()
    {
        yield return new WaitForSeconds(_TimeBeforeSwitchState);

        if (_StatesManager.GetStateKey() == this.stateKey)
        {
            _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.idle);
        }
        else
        {
            yield break;
        }
    }

    public override void FixedUpdateState()
    {
        
    }
}
