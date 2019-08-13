﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Projectile Projectile;
    public uint FireRateRPM;
    public int MaxHitPoints;
    [SerializeField]
    private int _currentHitPoints;
    [SerializeField]
    private uint _pauseBetweenShots;
    [SerializeField]
    private uint _shotTimer;

    // Start is called before the first frame update
    private void Start()
    {
        //fixed update 1 tick every 0.02 sec
        //1 second have 50 ticks
        //1 minute have 3000 ticks
        _pauseBetweenShots = 3000 / FireRateRPM;
        _shotTimer = 0;

        _currentHitPoints = MaxHitPoints;
    }

    private void FixedUpdate()
    {
        if(_shotTimer <= _pauseBetweenShots)
        {
            _shotTimer++;
        }
    }

    public void TakeHit(int damage)
    {
        _currentHitPoints = _currentHitPoints - damage;
        if(_currentHitPoints == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(GameObject shooter)
    {   
        if(_shotTimer >= _pauseBetweenShots)
        {
            Projectile projectile = Instantiate(Projectile);
            projectile.Shooter = shooter;
            _shotTimer = 0;
        }
    }
}