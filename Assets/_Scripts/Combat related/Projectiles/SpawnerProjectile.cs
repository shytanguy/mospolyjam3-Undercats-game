using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerProjectile : MonoBehaviour
{
    private Rigidbody2D _ProjectileRigidbody2D;

    [SerializeField] private float _speed=2f;

    [SerializeField] private Vector2 _direction=Vector2.down;

    [SerializeField] private float _timeBeforeExplosion = 1.5f;

    [SerializeField] private GameObject _toSpawn;
    private void Awake()
    {
        _ProjectileRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _ProjectileRigidbody2D.velocity = _speed * _direction;

        StartCoroutine(TimerForSpawn());
    }

    private IEnumerator TimerForSpawn()
    {
        yield return new WaitForSeconds(_timeBeforeExplosion);
        Instantiate(_toSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
