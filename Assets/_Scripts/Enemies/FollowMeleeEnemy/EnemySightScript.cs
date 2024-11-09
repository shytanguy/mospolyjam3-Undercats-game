using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightScript : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _playerLayer;
    private Transform _playerTransform;
    private bool _playerDetected;


    public bool CheckForPlayer()
    {
       
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, _detectionRadius, _playerLayer);

        if (playerCollider != null)
        {
            _playerDetected = true;
            _playerTransform = playerCollider.transform;
            return true;
        }
        else
        {
            _playerDetected = false;
            _playerTransform = null;
            return false;
        }
    }

    public bool IsPlayerDetected()
    {
        return _playerDetected;
    }

    public Transform GetPlayerTransform()
    {
        return _playerTransform;
    }

    private void OnDrawGizmosSelected()
    {
       
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
