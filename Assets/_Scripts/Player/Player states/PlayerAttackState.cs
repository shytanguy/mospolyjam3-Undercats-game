using System.Collections;
using System.Collections.Generic;

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

    [SerializeField] private float _knockbackForce = 5f;

    [SerializeField] private Vector2 _knockbackDirection;

    [SerializeField] private GameObject _counterEffectPrefab;

    [SerializeField] private float _timeZoom=0.6f;
    [SerializeField] private float _timePerZoom = 0.2f;
    [SerializeField] private float _zoomPercent = 1.3f;

    private bool _countered=false;
    [SerializeField] private AudioClip _parry;
    [SerializeField] private float _counterDamageMultiplier=1.2f;
    public override void EnterState()
    {
        _canInput = false;

        StartCoroutine(HitBoxSpawnDelay());
       
        _componentsManager.playerRigidbody.velocity = transform.right.normalized;
        StartCoroutine(InputDelay());

        StartCoroutine(SwitchStateDelay());

        _componentsManager.playerInput.actions["Attack"].performed += OnAttackInput;
    }
    public override void ExitState()
    {
        _componentsManager.playerRigidbody.velocity = Vector2.zero;
        _componentsManager.playerInput.actions["Attack"].performed -= OnAttackInput;
    }
    private void OnAttackInput(InputAction.CallbackContext context)
    {
        return;
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

                if (hitbox.Reflect(GetComponent<FactionScript>().userFaction))
                    Counter();
            }
            HealthScript hp;
            if (collider.TryGetComponent<HealthScript>(out hp))
            {
                if (_countered)
                {
                    hp.TakeDamage(_damage * _counterDamageMultiplier);
                }
                else
                hp.TakeDamage(_damage);
                Vector2 knockback=_knockbackDirection;
                if (transform.rotation.eulerAngles.y != 0)
                {
                    knockback = new Vector2(-knockback.x, knockback.y);
                }
                collider.GetComponent<Rigidbody2D>().AddForce(_knockbackForce * _knockbackDirection, ForceMode2D.Impulse);
            }
            _countered = false;
        }
      
    }
    private void Counter()
    {
        if (_countered) return;
        AudioManager.audioManager.PlaySound(_parry);
        _componentsManager.overlayScript.OverlayColorWhite();
        Instantiate(_counterEffectPrefab, transform.position, Quaternion.identity);
        TimeController.SetTimeScale(0.7f, this);
        CinemachineEffectsController.instance.ZoomCamera(_timePerZoom, _zoomPercent,_timeZoom);
        _countered = true;
        StartCoroutine(ResumeTimeCounter());
    }
    private IEnumerator ResumeTimeCounter()
    {
        yield return new WaitForSecondsRealtime(_timeZoom);
        TimeController.ResumeTime(this);
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
