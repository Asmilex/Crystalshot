﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    private float moveSpeed = 3;
    private float JumpSpeed = 9;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    //Put physica based movement in here
    private void FixedUpdate()
    {

        if (Input.GetKey("d"))
        {
            //Here goes walking to right animation
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }
        else if (Input.GetKey("a"))
        {
            //Here goes walking to left animation
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
        else
        {
            //Here goes walking stand by animation
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        }


        if (Input.GetKey("space") && isGrounded)
        {
            //Here goes jump animation
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpSpeed);
        }


    }
}