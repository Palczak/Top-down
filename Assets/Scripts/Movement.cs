using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _velocity;
    private float _angle;

    private Rigidbody2D _rigidBody;
    public float Speed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _angle = transform.rotation.z;
    }

    public void Move(Vector2 inputVector)
    {
        inputVector.x *= Speed;
        inputVector.y *= Speed;
        if(inputVector.x != 0 && inputVector.y != 0)
        {
            inputVector.x /= Mathf.Sqrt(2);
            inputVector.y /= Mathf.Sqrt(2);
        }
        _rigidBody.velocity = inputVector;
        _velocity = Mathf.Sqrt(Mathf.Pow(_rigidBody.velocity.x, 2) + Mathf.Pow(_rigidBody.velocity.y, 2));
    }

    public void Forward()
    {
        _rigidBody.velocity = transform.up * Speed;
    }

    public void LookAt(Vector2 inputVector)
    {
        inputVector = (Vector3)inputVector - transform.position;
        inputVector.Normalize();
        float rot_z = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
