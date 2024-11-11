using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbilityObject : MonoBehaviour
{
    [field: SerializeField] public AbilityAbstract ability { get; private set; }

    [SerializeField] private LayerMask _playerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (ability!=null)
            collision.gameObject.GetComponent<PlayerAbilitySwitcher>().AddAbility(ability);
        }
    }
}
