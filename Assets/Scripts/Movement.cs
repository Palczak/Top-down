using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _velocity;
    [SerializeField]
    private float _angle;


    private Rigidbody2D _rigidBody;
    

    public float Speed;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _angle = transform.rotation.z;
    }

    private void FixedUpdate()
    {
        
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
        Vector3 diff = Camera.main.ScreenToWorldPoint(inputVector) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
