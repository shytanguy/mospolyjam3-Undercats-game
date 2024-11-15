using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicController : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayers;

    private bool _combat=false;

    [SerializeField] private AudioClip _combatMusic;

    [SerializeField] private AudioClip _normalMusic;

    private int _enemies;

    private bool _coroutineStarted;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_enemyLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            _enemies++;
            SetCombatMusic();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_enemyLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            _enemies--;
            if (_enemies <= 0)
            {
                StartCoroutine(SetNormalMusicDelay());
            }
           
        }
    }
    private void SetNormalMusic()
    {
        if (_combat == false) return;
        _combat = false;
        AudioManager.audioManager.ChangeMusic(_normalMusic);
    }
    private IEnumerator SetNormalMusicDelay()
    {
        if (_coroutineStarted) yield break;
        _coroutineStarted = true;
        yield return new WaitForSeconds(2f);
        if (_enemies <= 0)
        {
            SetNormalMusic();
        }
        _coroutineStarted = false;
    }
    private void SetCombatMusic()
    {
        if (_combat == true) return;
        _combat = true;
        AudioManager.audioManager.ChangeMusic(_combatMusic);
    }
}
