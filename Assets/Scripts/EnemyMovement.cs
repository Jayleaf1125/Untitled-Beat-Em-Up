using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    public bool isMovingRight = true;

    Rigidbody2D _rb;
    Animator _animator;
    Vector2 _vectorMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector2(movementSpeed, 0) * Time.fixedDeltaTime);
        Flip();
        // _animator.SetBool("isWalking", true);
        
    }

    private void Flip()
    {
        if (_vectorMovement.x == -1 && isMovingRight)
        {
            isMovingRight = false;
            transform.Rotate(0, 180, 0);
            return;
        }

        if (_vectorMovement.x == 1 && !isMovingRight)
        {
            isMovingRight = true;
            transform.Rotate(0, 180, 0);
            return;
        }

    }

}


    

