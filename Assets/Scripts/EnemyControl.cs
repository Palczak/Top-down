using Assets.Scripts;
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
    private PathFinding _pathFinding;

    public GameObject Node;

    private void Start()
    {
        _layerMask = ~LayerMask.GetMask("Enemy");
        _target = GameObject.Find("Player");
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
        _pathFinding = new PathFinding(FindObjectOfType<GridNodes>().Nodes);

        var path = _pathFinding.FindPath(transform.position, _target.transform.position);
        var moveTo = new Vector2(path[path.Count - 1].X, path[path.Count - 1].Y);

        foreach (var node in path)
        {
            GameObject visualNode = Instantiate(Node);
            visualNode.transform.position = new Vector3(node.X, node.Y, 0);
        }
    }

    private void Update()
    {
        if (_target != null)
        {
            _movement.LookAt(_target.transform.position);
            var hit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, Mathf.Infinity, _layerMask);
            var path = _pathFinding.FindPath(transform.position, _target.transform.position);
            var moveTo = new Vector2(path[path.Count - 1].X, path[path.Count - 1].Y);
            //_movement.Move(moveTo);

            if (hit.rigidbody == _target.GetComponent<Rigidbody2D>())
            {
                _combat.Shoot(gameObject);
            }
        }
    }
}