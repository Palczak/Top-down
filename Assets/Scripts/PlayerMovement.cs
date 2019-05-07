using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _velocity;

    private Rigidbody2D _playerBody;

    public float Speed;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 inputVector;
        if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0)
        {
            inputVector = new Vector2((Input.GetAxisRaw("Horizontal") * Speed)/Mathf.Sqrt(2), (Input.GetAxisRaw("Vertical") * Speed) / Mathf.Sqrt(2));
        }
        else
        {
            inputVector = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, Input.GetAxisRaw("Vertical") * Speed);
        }

        _playerBody.velocity = inputVector;
        _velocity = Mathf.Sqrt(Mathf.Pow(_playerBody.velocity.x, 2) + Mathf.Pow(_playerBody.velocity.y, 2));

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
