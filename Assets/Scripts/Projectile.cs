using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement _movement;
    public float MaxExistenceTime;
    private float _existenceTime;
    private Collider2D _collider;
    public float Angle;
    public GameObject Shooter;
    public int Damage;
    private bool _used;

    private void Start()
    {
        _existenceTime = 0;
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _movement = GetComponent<Movement>();
        transform.position = Shooter.transform.position;
        transform.rotation = Shooter.transform.rotation;
        _used = false;
    }

    private void Update()
    {
        _existenceTime += Time.deltaTime;
        if (_existenceTime < MaxExistenceTime)
        {
            _movement.Move(transform.up);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
        else if ((collision.gameObject.tag == "Character" || collision.gameObject.tag == "Player") && collision.gameObject != Shooter)
        {
            if(!_used)
            {
                collision.gameObject.GetComponent<Combat>().TakeHit(Damage);
                _used = true;
            }
            Destroy(gameObject);
        }
    }
}
