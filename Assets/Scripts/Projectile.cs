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

    private void Start()
    {
        _existenceTime = 0;
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _movement = GetComponent<Movement>();
        //Angle = Angle * Mathf.Rad2Deg;
        transform.position = Shooter.transform.position + Shooter.transform.up;
        transform.rotation = Shooter.transform.rotation;
        //transform.Rotate()
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.name == "Wall")
        //{
        //    Destroy(gameObject);
        //}

        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _existenceTime++;
        if (_existenceTime < MaxExistenceTime)
        {
            _movement.Forward();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
