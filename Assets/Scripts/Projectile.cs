using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private Movement _movement;
    public int MaxExistenceTime;
    private int _existenceTime;
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
        //Angle = Angle * Mathf.Rad2Deg;
        transform.position = Shooter.transform.position;
        transform.rotation = Shooter.transform.rotation;
        //transform.Rotate()
        _used = false;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Character" && collision.gameObject != Shooter)
        {
            if(!_used)
            {
                collision.gameObject.GetComponent<Combat>().TakeHit(Damage);
                _used = true;
            }
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _existenceTime++;
        if (_existenceTime < MaxExistenceTime)
        {
            _movement.Move(transform.up);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
