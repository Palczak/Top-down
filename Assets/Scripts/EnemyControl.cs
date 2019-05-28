using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject Target;
    private Movement _movement;
    private Combat _combat;

    // Start is called before the first frame update
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
    }

    // Update is called once per frame
    private void Update()
    {
        _movement.LookAt(Target.transform.position);
    }

}