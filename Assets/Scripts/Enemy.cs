using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private PlayerMovement target;
    private Vector2 moveDirection;
    private LayerMask isitGround;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerMovement>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        
    }

    void Update()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
       
    }

    
}
