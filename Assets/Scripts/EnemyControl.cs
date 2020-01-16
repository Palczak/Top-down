using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyControl : MonoBehaviour
{
    private GameObject _target;
    private Movement _movement;
    private Combat _combat;
    private int _layerMask;
    private PathFinding _pathFinding;

    private void Start()
    {
        _layerMask = ~LayerMask.GetMask("Enemy");
        //_target = GameObject.Find("Player");
        _target = GameObject.FindGameObjectWithTag("Player");
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
        _pathFinding = new PathFinding(FindObjectOfType<GridNodes>().Nodes);
    }

    private void Update()
    {
        if (_target != null)
        {
            var path = _pathFinding.FindPath(transform.position, _target.transform.position);
            if (path.Count() != 0)
            {
                var moveTo = new Vector2(path.Last().X, path.Last().Y);
                moveTo -= (Vector2)transform.position;
                _movement.Move(moveTo);
            }

            if (Config.HardMode)
            {
                Vector2 targetVelocity = _target.GetComponent<Rigidbody2D>().velocity;
                float missileSpeed = _combat.Projectile.GetComponent<Movement>().Speed * 0.75f;

                float a = (targetVelocity.x * targetVelocity.x) + (targetVelocity.y * targetVelocity.y) - (missileSpeed * missileSpeed);
                float b = 2 * (targetVelocity.x * (_target.transform.position.x - gameObject.transform.position.x)
                    + targetVelocity.y * (_target.transform.position.y - gameObject.transform.position.y));
                float c = ((_target.transform.position.x - gameObject.transform.position.x) * (_target.transform.position.x - gameObject.transform.position.x)) +
                    ((_target.transform.position.y - gameObject.transform.position.y) * (_target.transform.position.y - gameObject.transform.position.y));
  
                float disc = b * b - (4 * a * c);

                float t1 = (-1 * b + Mathf.Sqrt(disc)) / (2 * a);
                float t2 = (-1 * b - Mathf.Sqrt(disc)) / (2 * a);
                float t = Mathf.Max(t1, t2);// let us take the larger time value 
                float aimX = (targetVelocity.x * t) + _target.transform.position.x;
                float aimY = _target.transform.position.y + (targetVelocity.y * t);
                _movement.LookAt(new Vector2(aimX, aimY));
                _combat.Shoot(gameObject);//now position the gameObject

            }
            else
            {
                _movement.LookAt(_target.transform.position);
                var rayHit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, Mathf.Infinity, _layerMask);
                if (rayHit.rigidbody == _target.GetComponent<Rigidbody2D>())
                {
                    _combat.Shoot(gameObject);
                }
            }
        }
    }
}