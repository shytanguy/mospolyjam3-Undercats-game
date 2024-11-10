using System.Collections;
using UnityEngine;

public class EnemyAbilityHandler : MonoBehaviour
{
    [SerializeField] private AbilityAbstract ability;
    private bool abilityUsedOnce = false;
    private void Start()
    {
        if (ability != null && ability.useOnceOnSpawn)
        {
            ability.TurnOnAbility();
            abilityUsedOnce = true;
        }
    }

    private void Update()
    {
        if (ability != null && !ability.useOnceOnSpawn && !abilityUsedOnce)
        {
               
                    ability.TurnOnAbility();
                    
                    StartCoroutine(AbilityCooldownCoroutine());
                
        }
    }

    private IEnumerator AbilityCooldownCoroutine()
    {
        yield return new WaitForSeconds(ability.cooldownTime);
        abilityUsedOnce = false;
        ability.TurnOffAbility();
    }
}