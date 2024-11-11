using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTextTrigger : MonoBehaviour
{
    [SerializeField] private string _text;
    [SerializeField] private float _characterSpeed;

    [SerializeField] private float _timeStaying;

    [SerializeField] private LayerMask _playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            PlotText.instance.SetText(_characterSpeed, _timeStaying, _text);
            Destroy(gameObject);
        }
    }
}
