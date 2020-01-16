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
        inputVector *= Speed;
        //inputVector *= (Time.deltaTime);

        if (inputVector.x != 0 && inputVector.y != 0)
        {
            inputVector.x /= Mathf.Sqrt(2);
            inputVector.y /= Mathf.Sqrt(2);
        }

        Vector3 moveVector = new Vector3(inputVector.x, inputVector.y);

        _rigidBody.velocity = moveVector;
        //_rigidBody.MovePosition(transform.position + moveVector);
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
