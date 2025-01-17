﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement _movement;
    private Collider2D _collider;
    public float Angle;
    public GameObject Shooter;
    public int Damage;
    private bool _used;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _movement = GetComponent<Movement>();
        transform.position = Shooter.transform.position;
        transform.rotation = Shooter.transform.rotation;
        _used = false;
    }

    private void Update()
    {
        _movement.Move(transform.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null)
            return;
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
        else if (IsCharacterHit(collision) && !IsFriendlyFire(collision))
        {
            if (!_used)
            {
                collision.gameObject.GetComponent<Combat>().TakeHit(Damage);
                _used = true;
            }
            Destroy(gameObject);
        }
    }

    private bool IsCharacterHit(Collider2D collision)
    {
        return collision.gameObject.tag == "Character" || collision.gameObject.tag == "Player";
    }

    private bool IsFriendlyFire(Collider2D collision)
    {
        return collision.gameObject == Shooter || collision.gameObject.tag == Shooter.tag;
    }
}
