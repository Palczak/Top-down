using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameObject _player;
    private Movement _movement;
    private Combat _combat;
    private Camera _playerCamera;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
        _playerCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        _movement.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetButton("Fire1"))
        {
            _combat.Shoot(gameObject);
        }
        MoveCamera();
    }

    private void FixedUpdate()
    {
        _movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void MoveCamera()
    {
        var x = _player.transform.position.x;
        var y = _player.transform.position.y;
        var z = -10;

        _playerCamera.transform.position = new Vector3(x, y, z);
    }
}
