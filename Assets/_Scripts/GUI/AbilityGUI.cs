using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGUI : MonoBehaviour
{
    [SerializeField] private Image _currentAbilityIcon;

    [SerializeField] private TextMeshProUGUI _currentAbilityName;

    [SerializeField] private Image _nextAbilityIcon;

    [SerializeField] private float animationDuration = 0.5f;

    [SerializeField] private PlayerAbilitySwitcher _playerAbilitySwitcher;

    private RectTransform _currentAbilityRect;

    private RectTransform _nextAbilityRect;

    private Vector2 _currentAbilityStartPosition;

    private Vector2 _nextAbilityStartPosition;

    private void Awake()
    {
        _currentAbilityRect = _currentAbilityIcon.GetComponent<RectTransform>();

        _nextAbilityRect = _nextAbilityIcon.GetComponent<RectTransform>();

        _currentAbilityStartPosition = _currentAbilityRect.anchoredPosition;

        _nextAbilityStartPosition = _nextAbilityRect.anchoredPosition;
    }

    private void OnEnable()
    {
        if (_playerAbilitySwitcher == null) return;
      
        _playerAbilitySwitcher.OnSwitchingAbility += AnimateAbilitySwitch;
        if (_playerAbilitySwitcher._playerAbilities.Count == 0)
        {
            _nextAbilityIcon.transform.parent.gameObject.SetActive(false);
            _currentAbilityIcon.gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }
        UpdateAbilityDisplay(_playerAbilitySwitcher.GetNextAbility());
    }

    private void AnimateAbilitySwitch(AbilityAbstract newAbility)
    {
        UpdateAbilityDisplay(newAbility);
    }

    

    private void UpdateAbilityDisplay(AbilityAbstract newAbility)
    {
        if (newAbility == null) return;
     
        _currentAbilityIcon.gameObject.transform.parent.gameObject.SetActive(true);
        _currentAbilityIcon.sprite = newAbility.Icon;

        _currentAbilityName.text = newAbility.Name;

        AbilityAbstract nextAbility = _playerAbilitySwitcher.GetNextAbility();
        if (nextAbility != null)
        {
            _nextAbilityIcon.transform.parent.gameObject.SetActive(true);
            _nextAbilityIcon.sprite = nextAbility.Icon;
        }
        else
        {
            _nextAbilityIcon.transform.parent.gameObject.SetActive(false);
            _nextAbilityIcon.sprite = null;
        }
    }

    private void OnDisable()
    {
        if (_playerAbilitySwitcher != null)
        {
            _playerAbilitySwitcher.OnSwitchingAbility -= AnimateAbilitySwitch;
        }
    }
}
