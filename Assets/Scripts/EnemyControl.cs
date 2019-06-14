using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject Target;
    private Movement _movement;
    private Combat _combat;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
    }

    private void Update()
    {
        if(Target != null)
        {
            _movement.LookAt(Target.transform.position);
            _combat.Shoot(gameObject);
        }
    }

}