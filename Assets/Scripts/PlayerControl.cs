using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Movement _movement;
    private Combat _combat;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
    }

    private void Update()
    {
        //_movement.LookAt(Input.mousePosition);
        //transform.LookAt(Input.mousePosition);
        //transform.position = Input.mousePosition;
        _movement.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetButton("Fire1"))
        {
            _combat.Shoot(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
