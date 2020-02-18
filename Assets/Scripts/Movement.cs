using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public float Speed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 inputVector)
    {
        var moveVector = inputVector.normalized;
        moveVector *= Speed;
        _rigidBody.velocity = moveVector;
    }

    public void Forward()
    {
        Move(transform.up);
    }

    public void LookAt(Vector2 inputVector)
    {
        inputVector = (Vector3)inputVector - transform.position;
        inputVector.Normalize();
        float rot_z = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
