using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private FinishController finish;
    private LevelSword levelSword;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] Animator chestAnimator;


    private float _horizontal = 0f; 
    private bool _isGrounded = false;
    private bool _isJump = false;
    private bool _isFlip = false;
    private bool _isFinish = false;
    private bool _isLevelSword = false;
    

    const float speedXMultiplier = 50f;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishController>();
        levelSword = FindObjectOfType<LevelSword>();//Ищет на сцене объект с именем LevelSword
    }

    private void Update()
    {
       _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(_horizontal));
        
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _isJump = true;
        }

        if (Input.GetKey(KeyCode.E)) 
        {
            if (_isFinish)  
            {
                finish.FinishLevel();
            }
            if (_isLevelSword)
            {
                levelSword.ActivateLevelSword();
            }
        }
    }

    private void FixedUpdate()
    {
       rb.velocity = new Vector2(_horizontal * speed * speedXMultiplier * Time.fixedDeltaTime, rb.velocity.y);

        if (_isJump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            animator.Play("Player_jump");
            _isGrounded = false;
            _isJump = false;
        }

        if (_horizontal > 0f && _isFlip)
        {
            Flip();
        }
        else if (_horizontal < 0f && !_isFlip)
        {
            Flip();
        }
    }
    void Flip()
    {
        _isFlip = !_isFlip;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded=true;
            animator.Play("Player_idle");
        }
        else
        {
            animator.Play("Player_idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelSword levelSwordTemp = collision.GetComponent<LevelSword>();    

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Нажмите E");
            _isFinish = true;
        }
        if (levelSwordTemp != null)
        {
            Debug.Log("Вы нашли ключ");
            _isLevelSword = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LevelSword levelSwordTemp = collision.GetComponent<LevelSword>();

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Вы отошли от Финиша");
            _isFinish = false;
            chestAnimator.Play("CloseCh");
        }
        if (levelSwordTemp != null)
        {
            _isLevelSword = false;
        }
    }
}
