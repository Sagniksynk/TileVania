using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 playerInput;
    Rigidbody2D rb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] float climbSpeed = 3.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask ladderLayer;
    [SerializeField] LayerMask enemyLayer;
    Animator animator;
    private string running_anim = "isRunning";
    private string climb_anim = "isClimbing";
    CapsuleCollider2D capsuleCollider;
    private bool isClimbing = false;
    private float gravityAtStart;
    private bool isAlive = true;
    
    void Start()
    {
     rb = GetComponent<Rigidbody2D>(); 
     animator = GetComponent<Animator>();
     capsuleCollider = GetComponent<CapsuleCollider2D>();
     gravityAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    
        private void FixedUpdate()
        {
            if (!isAlive) { return; }
            if (!isClimbing)
            {
                Move();
                Flip();
            }

            Ladder();
            Death();
        }
        
        
    
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        playerInput = value.Get<Vector2>();
        //Debug.Log(playerInput);
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!capsuleCollider.IsTouchingLayers(groundLayer))
        {
            //Debug.Log(layer);
            return;
        }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpForce);
        }
    }
    public void Move()
    {
        if (!isAlive) { return; }
        Vector2 playerMove = new Vector2(playerInput.x*speed, rb.velocity.y);
        rb.velocity = playerMove;
        bool hasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        //animator.SetBool(climb_anim, false);
        animator.SetBool(running_anim, hasHorizontalSpeed);
    }
    public void Flip()
    {
        if (!isAlive) { return; }
        bool hasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; 
        if (hasHorizontalSpeed)
        {
           transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f); //flipping
        }
    }
    public void Ladder()
    {
        if (!isAlive) { return; }
        if (!capsuleCollider.IsTouchingLayers(ladderLayer))
        {
            
            isClimbing = false;
            rb.gravityScale = gravityAtStart;
            animator.SetBool(climb_anim, false);
            return;
        }
        else if (capsuleCollider.IsTouchingLayers(ladderLayer) && Input.GetKey(KeyCode.E)){
            isClimbing = true;
            Vector2 playerClimb = new Vector2(rb.velocity.x, playerInput.y * climbSpeed);
            rb.velocity = playerClimb;
            rb.gravityScale = 0f;

            animator.SetBool(climb_anim, true);
        }
        
    }
    private void Death()
    {
        if (capsuleCollider.IsTouchingLayers(enemyLayer))
        {
            isAlive = false;
        }
    }
    

}
