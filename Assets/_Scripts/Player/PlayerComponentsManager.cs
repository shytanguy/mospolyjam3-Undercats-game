using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponentsManager : MonoBehaviour
{
     [HideInInspector] public Rigidbody2D playerRigidbody { get; private set; }
     [HideInInspector] public PlayerInput playerInput { get; private set; }
     [HideInInspector]   public PlayerStatesManager statesManager { get; private set; }
     [field: SerializeField] public Animator playerAnimator { get; private set; }
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
   
        playerInput = GetComponent<PlayerInput>();

        statesManager = GetComponent<PlayerStatesManager>();


    }


   
}
