using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionScript : MonoBehaviour
{
    private PlayerComponentsManager _components;

    [SerializeField] private float _radius=2f;

    [SerializeField] private LayerMask _interactable;

    [SerializeField] private GameObject _GUIGuide;
    private void Awake()
    {
        _components = GetComponent<PlayerComponentsManager>();
    }
    private void OnEnable()
    {
        _components.playerInput.actions["Interact"].performed += OnInput;
    }
    private void OnDisable()
    {
        _components.playerInput.actions["Interact"].performed -= OnInput;
    }
    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _radius, _interactable);
        if (_GUIGuide.activeInHierarchy==false&& collider != null)
        {
            _GUIGuide.SetActive(true);
        }
        else if (collider==null&&_GUIGuide.activeInHierarchy==true)
        {
            _GUIGuide.SetActive(false);
        }
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
