using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionScript : MonoBehaviour
{
    private PlayerComponentsManager _components;

    [SerializeField] private float _radius=2f;

    [SerializeField] private LayerMask _interactable;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void OnInput(InputAction.CallbackContext context)
    {
        CheckInteraction();
    }
    private void CheckInteraction()
    {
       Collider2D collider= Physics2D.OverlapCircle(transform.position, _radius, _interactable);
        if (collider != null)
        {
            collider.GetComponent<IInteraction>().OnInteraction();
        }
    }
}
