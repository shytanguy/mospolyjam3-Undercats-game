using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyComponentManager : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D EnemyRigidbody { get; private set; }
    [HideInInspector] public EnemyStatesManager statesManager { get; private set; }
    [field: SerializeField] public Animator EnemyAnimator { get; private set; }

    [HideInInspector] public EnemySightScript sightScript { get; private set; }

    private void Awake()
    {
        EnemyRigidbody = GetComponent<Rigidbody2D>();

        statesManager = GetComponent<EnemyStatesManager>();

        sightScript = GetComponent<EnemySightScript>();
    }

}
