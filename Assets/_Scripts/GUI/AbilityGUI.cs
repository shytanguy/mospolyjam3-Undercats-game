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

        UpdateAbilityDisplay(_playerAbilitySwitcher.GetNextAbility());
    }

    private void AnimateAbilitySwitch(AbilityAbstract newAbility)
    {
        StopAllCoroutines();

        StartCoroutine(SwitchAnimation(newAbility));
    }

    private IEnumerator SwitchAnimation(AbilityAbstract newAbility)
    {

        Sprite initialCurrentIcon = _currentAbilityIcon.sprite;

        string initialCurrentName = _currentAbilityName.text;

        Sprite initialNextIcon = _nextAbilityIcon.sprite;

        
        _nextAbilityIcon.sprite = initialCurrentIcon;

        _currentAbilityName.text = newAbility.Name;

        _currentAbilityIcon.sprite = newAbility.Icon;

        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / animationDuration;

            _currentAbilityRect.anchoredPosition = Vector2.Lerp(_currentAbilityStartPosition, _nextAbilityStartPosition, t);

            _nextAbilityRect.anchoredPosition = Vector2.Lerp(_nextAbilityStartPosition, _currentAbilityStartPosition, t);

            yield return null;
        }

     
        _currentAbilityRect.anchoredPosition = _currentAbilityStartPosition;

        _nextAbilityRect.anchoredPosition = _nextAbilityStartPosition;


        UpdateAbilityDisplay(newAbility);
    }

    private void UpdateAbilityDisplay(AbilityAbstract newAbility)
    {
        if (newAbility == null) return;

        _currentAbilityIcon.sprite = newAbility.Icon;

        _currentAbilityName.text = newAbility.Name;

        AbilityAbstract nextAbility = _playerAbilitySwitcher.GetNextAbility();
        if (nextAbility != null)
        {
            _nextAbilityIcon.sprite = nextAbility.Icon;
        }
        else
        {
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
