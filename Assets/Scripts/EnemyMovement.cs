using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.0f;
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();   
    }

   
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        Flip();
    }
    void Flip()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
    }
}
