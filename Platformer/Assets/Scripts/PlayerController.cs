using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] GameObject attackHitBox;
    [SerializeField] GameObject blockBox;

    bool isGrounded;
    bool isAttacking = false;   
    bool isBlocked = false; 
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        attackHitBox.SetActive(false);
        blockBox.SetActive(false);  
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;

            int choose = Random.Range(1, 3);
            animator.Play("Player_attack" + choose);

            StartCoroutine(DoAttack());
        }
        else if (Input.GetButton("Fire2") && !isAttacking)
        {
            isAttacking = true;

            animator.Play("Player_block");

            StartCoroutine(DoBlock());
        }
    }
    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("GameObject")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isGrounded && !isAttacking)
                animator.Play("Player_run");

            transform.localScale = new Vector3(1,1,1); 
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isGrounded && !isAttacking)
                animator.Play("Player_run");

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (isGrounded)
            {
                if (!isAttacking)
                {
                    animator.Play("Player_idle");
                }
            }
               
        }


        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.Play("Player_jump");
        }
    }

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    IEnumerator DoBlock()
    {
        blockBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        blockBox.SetActive(false);
        isAttacking = false;
    }
}
