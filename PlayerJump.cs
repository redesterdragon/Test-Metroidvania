using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{  
    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping; 

    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float rad0Circle;
    [SerializeField] private LayerMask WhatIsGround;
    public bool grounded;

    [Header("components")]
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        //what it means to be grounded 
        grounded = Physics2D.OverlapCircle(groundCheck.position,rad0Circle,WhatIsGround);
        
        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        //if we press the jump button
        if (Input.GetButtonDown("Jump") && grounded)
        {
            //jump!!!
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
        }
        //if we hold the jump button 
        if(Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0) )
        {
        
            //jump!!!
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        //if we release the jump button
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, rad0Circle);
    }
}