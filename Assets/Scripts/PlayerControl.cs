using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Movement _movement;
    private Combat _combat;
    void Start()
    {
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
    }

    private void Update()
    {
        _movement.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetButton("Fire1"))
        {
            _combat.Shoot(gameObject);
        }
    }

    void FixedUpdate()
    {
        _movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
