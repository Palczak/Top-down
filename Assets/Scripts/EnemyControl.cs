using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControl : MonoBehaviour
{
    private GameObject _target;
    private Movement _movement;
    private Combat _combat;
    private int _layerMask;

    private void Start()
    {
        _layerMask = ~LayerMask.GetMask("Enemy");
        _target = GameObject.Find("Player");
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
    }

    private void Update()
    {
        if (_target != null)
        {
            _movement.LookAt(_target.transform.position);
            var hit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, Mathf.Infinity, _layerMask);

            if (hit.rigidbody == _target.GetComponent<Rigidbody2D>())
            {
                _combat.Shoot(gameObject);
            }
        }
    }

}