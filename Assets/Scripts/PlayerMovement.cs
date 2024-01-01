using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 playerInput;
    Rigidbody2D rb;
    [SerializeField] private float speed = 5.0f;
    Animator animator;
    private string running_anim = "isRunning";
    void Start()
    {
     rb = GetComponent<Rigidbody2D>(); 
     animator = GetComponent<Animator>();
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
    public void Move()
    {
        Vector2 playerMove = new Vector2(playerInput.x*speed, rb.velocity.y);
        rb.velocity = playerMove;
        bool hasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool(running_anim, hasHorizontalSpeed);
    }
    public void Flip()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; 
        if (hasHorizontalSpeed)
        {
           transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); //flipping
        }
    }
    
}
