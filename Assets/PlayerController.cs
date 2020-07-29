using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;

    [Range(1,10)]
    public float moveSpeed = 3;

    [Range(1, 10)]
    public float JumpSpeed = 9;

    bool isGrounded = true;



    public const int max_bullets = 3;
    public int bullets_avaliable;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bullets_avaliable = max_bullets;
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
