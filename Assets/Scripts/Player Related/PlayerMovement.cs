﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Movement Code
    public float moveSpeed;
    private Rigidbody2D rb;
    public float velX; 
    public float velY;
    [SerializeField]private bool facingRight = true;
   //Jumping Code 
    [SerializeField]private bool isGrounded;
    public bool isJumping;

   public Transform feetPos;
   public float checkRadius;
   public LayerMask whatisGround;
   
   public float jumpForce;

   private float jumpTimeCounter;
   public float jumpTime;
   
   //Ladder Code
   public LayerMask whatIsLadder;
  [SerializeField] private bool isClimbing;
   public float distance;
   private float inputVertical;
   public float ladderSpeed;

   public Joystick joystick;
   public Joystick ladderJoystick;
   //Animator Code
   public Animator animator;
   
   public int damage;

   //Player Health
  public int playerHealth;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Health>().deathEvent += Death;

    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= .1f)
        {
            velX = moveSpeed;
        }
        else if (joystick.Horizontal <= -.1f)
        {
            velX = -moveSpeed;
        }
        else
        {
            velX = 0;
        }
        //move left and right
        //velX = joystick.Horizontal * moveSpeed;
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * moveSpeed, velY);
        animator.SetFloat("Speed",Mathf.Abs(joystick.Horizontal));
        
        LadderMovement();
        Jump();

    }

     void LateUpdate()
     {
         //method that changes the way the player is facing depending on what key they press
         Vector3 localScale = transform.localScale;
         if (velX > 0)
         {
             facingRight = true;
         }
         else if (velX < 0)
         {
             facingRight = false;
         }
         if (((facingRight) && (localScale.x <0)) || ((!facingRight)) && localScale.x > 0)
         {
             localScale.x *= -1;
         }
         transform.localScale = localScale;

     }

     public void Jump()
     {
         //jumping code
         //checks to see if player is on the float using OverlapCircle
         isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatisGround);
         float verticalMove= joystick.Vertical;
         if (isGrounded == true && verticalMove >= .5f) 
         {
             isJumping = true;
             jumpTimeCounter = jumpTime;
             //rb will jump go up at a certain speed
             rb.velocity = Vector2.up * jumpForce;
         }

         if (verticalMove >= .5f && isJumping ==true)
         {
             if (jumpTimeCounter > 0)
             {
                 //rb will jump go up at a certain speed
                 rb.velocity = Vector2.up * jumpForce;
                 jumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 isJumping = false;
                
             }
            
         }

         if (verticalMove >= .5f)
         {
             isJumping = false;
         }
        
     }

     public void LadderMovement()
     {
         // Going up ladders using Raycasts and LayerMasks
         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
         float verticalClimb= ladderJoystick.Vertical;
         float horizontalClmb = ladderJoystick.Horizontal;
         if (hitInfo.collider != null)
         {
             if (verticalClimb >= .5f)
             {
                 isClimbing = true;
             }
             else
             {
                 //being able to move off the ladder when needed
                 if (horizontalClmb >= .1f  || horizontalClmb <= - .1f)
                 {
                     isClimbing = false;
                 }
             }
         }
//being able to go up the ladder
         if (isClimbing == true && hitInfo.collider != null)
         {
             
             rb.velocity = new Vector2(rb.velocity.x,verticalClimb * ladderSpeed);
             rb.gravityScale = 0;
         }
         else
         {
             rb.gravityScale = 5;
         }

     }

      void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.GetComponent<Health>() != null)
         {
            
             other.gameObject.GetComponent<Health>().ChangeHealth(-damage);
         }
        
     }

     void Death(Health health)
     {
         //Destroy(gameObject);
         Debug.Log("Dead!");
     }
     

   
}
