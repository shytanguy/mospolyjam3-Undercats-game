using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _walkEffect;

    private EnemyComponentManager _components;

    [SerializeField] private AudioClip[] _walkSounds;

    [SerializeField] private AudioClip _attack;

    private EnemyStatesManager.EnemyStates _currentKey;
    private void Awake()
    {
        _components = GetComponent<EnemyComponentManager>();
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

    private void ManageEffects(EnemyStatesManager.EnemyStates PreviousState, EnemyStatesManager.EnemyStates NewState)
    {
        _currentKey = NewState;
        switch (NewState)
        {


        }
        switch (PreviousState)
        {

        }
    }
    private void ManageAnimations(EnemyStatesManager.EnemyStates PreviousState, EnemyStatesManager.EnemyStates NewState)
    {

        switch (NewState)
        {
           
            case EnemyStatesManager.EnemyStates.follow:  StartCoroutine(PlaySoundRepeat(_walkSounds, EnemyStatesManager.EnemyStates.follow)); break;
            case EnemyStatesManager.EnemyStates.attack:  AudioManager.audioManager.PlaySound(_attack); break;
          
        }

    }
    private IEnumerator PlaySoundRepeat(AudioClip[] clips, EnemyStatesManager.EnemyStates key)
    {
        while (_currentKey == key)
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
