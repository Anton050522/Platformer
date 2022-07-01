using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public Transform groudChek;
    public float chekRadius;
    public LayerMask whatIsGround;

    private int extraJump;
    public int extraJumpValue;

    void Start()
    {
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groudChek.position, chekRadius, whatIsGround);


        Flip();
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
       
    }

    private void Update()
    {
        if (isGrounded==true)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
       
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
