using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class VisualsModule : MonoBehaviour
{
  private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Sprite _initalSprite;

  [SerializeField]  private Sprite _turnOffSprite;
  [SerializeField]  private Color _turnOffColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        TryGetComponent<Animator>(out _animator);
        _initalSprite = _spriteRenderer.sprite;


    }
    private void OnEnable()
    {
        VisualModuleController.OnActivated += TurnGraphicsOn;
        VisualModuleController.OnDeactivated += TurnGraphicsOff;
    }
    private void OnDisable()
    {
        VisualModuleController.OnActivated -= TurnGraphicsOn;
        VisualModuleController.OnDeactivated -= TurnGraphicsOff;
    }
    private void TurnGraphicsOn()
    {
        _spriteRenderer.sprite = _initalSprite;
        _spriteRenderer.color = Color.white;
        if (_animator!=null)
        _animator.enabled = true;
    }
    private void TurnGraphicsOff()
    {
        _spriteRenderer.sprite = _turnOffSprite;
        _spriteRenderer.color = _turnOffColor;
        if (_animator != null)
            _animator.enabled =false;
    }

}
