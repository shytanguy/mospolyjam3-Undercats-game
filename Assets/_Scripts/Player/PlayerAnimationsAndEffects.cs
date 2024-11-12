using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsAndEffects : MonoBehaviour
{
    private PlayerComponentsManager _components;

    [SerializeField] private ParticleSystem _walkEffect;
  

  
    [SerializeField] private AudioClip[] _walkSounds;
    [SerializeField] private AudioClip[] _attackSounds;

    [SerializeField] private AudioClip _dash;

    private PlayerStatesManager.PlayerStates _currentKey;
    private void Awake()
    {
        _components = GetComponent<PlayerComponentsManager>();
    }

    private void OnEnable()
    {
        _components.statesManager.StateChangedEvent += ManageEffects;
        _components.statesManager.StateChangedEvent += ManageAnimations;
  
    }
    private void OnDisable()
    {
        _components.statesManager.StateChangedEvent -= ManageEffects;
        _components.statesManager.StateChangedEvent -= ManageAnimations;
   
    }
 
    private void ManageEffects(PlayerStatesManager.PlayerStates PreviousState,PlayerStatesManager.PlayerStates NewState)
    {
        _currentKey = NewState;
        switch (NewState)
        {
           
            
        }
        switch (PreviousState)
        {
    
        }
    }
    private void ManageAnimations(PlayerStatesManager.PlayerStates PreviousState, PlayerStatesManager.PlayerStates NewState)
    {
        
        switch (NewState)
        {
            case PlayerStatesManager.PlayerStates.idle: _components.playerAnimator.Play("idle"); break;
            case PlayerStatesManager.PlayerStates.walk: _components.playerAnimator.Play("walk");StartCoroutine( PlaySoundRepeat(_walkSounds, PlayerStatesManager.PlayerStates.walk)); break;
            case PlayerStatesManager.PlayerStates.dash: _components.playerAnimator.Play("dash");AudioManager.audioManager.PlaySound(_dash); break;
            case PlayerStatesManager.PlayerStates.attackMiddle:
                _components.playerAnimator.Play("attackMid");
                AudioManager.audioManager.PlaySound(_attackSounds[UnityEngine.Random.Range(0, _attackSounds.Length)]); break;
            case PlayerStatesManager.PlayerStates.attackTop: _components.playerAnimator.Play("attackTop"); AudioManager.audioManager.PlaySound(_attackSounds[UnityEngine.Random.Range(0, _attackSounds.Length)]); break;
            case PlayerStatesManager.PlayerStates.attackDown: _components.playerAnimator.Play("attackDown"); AudioManager.audioManager.PlaySound(_attackSounds[UnityEngine.Random.Range(0, _attackSounds.Length)]); break;

        }
       
    }
    private IEnumerator PlaySoundRepeat(AudioClip[] clips,PlayerStatesManager.PlayerStates key)
    {
        while (_currentKey==key)
        {
            yield return new WaitForSeconds(0.4f);
            AudioManager.audioManager.PlaySound(clips[Random.Range(0, clips.Length)]);
        }
    }
    private IEnumerator TurnOffEffectDelay(ParticleSystem particleSystem)
    {
        particleSystem.Play();
        yield return new WaitForEndOfFrame();
        particleSystem.Stop();
    }
}
