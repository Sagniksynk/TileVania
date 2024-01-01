using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 playerInput;
    Rigidbody2D rb;
    [SerializeField] private float speed = 5.0f;
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Flip();
        
    }
    void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
        //Debug.Log(playerInput);
    }
    void Move()
    {
        Vector2 playerMove = new Vector2(playerInput.x*speed, rb.velocity.y);
        rb.velocity = playerMove;
    }
    void Flip()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
    
}
