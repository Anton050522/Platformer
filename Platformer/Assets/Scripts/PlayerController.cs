using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private FinishController finish;
    private LevelKey levelKey;
    private FakeKey fakeKey;
    private LevelArm levelArm;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] Animator chestAnimator;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private Transform _playerModelTransform;

    [SerializeField] private GameObject _pressECanvas; // нажмите Е
    [SerializeField] private GameObject _finischHintCanvas; // при открытии финиша
    [SerializeField] private GameObject _hintOpenLadderCanvas; // мы открыли мост

    private float _horizontal = 0f; 
    private bool _isGrounded = false;
    private bool _isJump = false;
    private bool _isFlip = false;
    private bool _isFinish = false;
    private bool _isLevelkey = false;
    private bool _isFakeKey = false;
    private bool _isLevelArm = false;

    const float speedXMultiplier = 50f;

    public bool IsFlip => _isFlip;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishController>();
        levelKey = FindObjectOfType<LevelKey>();//Ищет на сцене объект с именем LevelSword
        fakeKey = FindObjectOfType<FakeKey>();
        levelArm = FindObjectOfType<LevelArm>();
    }

    private void Update()
    {
       _horizontal = Input.GetAxis("Horizontal");
        _playerAnimator.SetFloat("speed", Mathf.Abs(_horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isJump = true;
            _jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (_isFinish)  
            {
                finish.FinishLevel();
            }
            if (_isLevelkey)
            {
                levelKey.ActivateLevelKey();
            }
            if (_isFakeKey)
            {
                fakeKey.ActivateFakeKey();
            }
            if (_isLevelArm)
            {
                levelArm.ActivateLevelArm();
            }
        }
    }

    private void FixedUpdate()
    {
       rb.velocity = new Vector2(_horizontal * speed * speedXMultiplier * Time.fixedDeltaTime, rb.velocity.y);

        if (_isJump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            _playerAnimator.Play("Player_jump");
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
        Vector3 playerScale = _playerModelTransform.localScale;
        playerScale.x *= -1;
        _playerModelTransform.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded=true;
            _playerAnimator.Play("Player_idle");
        }
        else
        {
            _playerAnimator.Play("Player_idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelKey levelKeyTemp = collision.GetComponent<LevelKey>();  
        FakeKey fakeKey = collision.GetComponent<FakeKey>();
        LevelArm levelArm = collision.GetComponent<LevelArm>();

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("Вы рядом с финишом\nНажмите E");
            _pressECanvas.SetActive(true);
            _isFinish = true;
        }
        if (levelKeyTemp != null)
        {
            Debug.Log("Вы нашли ключ. \n Нажмите Е");
            _pressECanvas.SetActive(true);
            _isLevelkey = true;
        }
        if (fakeKey != null)
        {
            Debug.Log("Вы нашли ключ. \n Нажмите Е");
            _pressECanvas.SetActive(true);
            _isFakeKey = true;
        }
        if (levelArm != null)
        {
            Debug.Log("Вы нашли Переключатель. \n Нажмите Е");
            _pressECanvas.SetActive(true);
            _isLevelArm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        LevelKey levelSwordTemp = collision.GetComponent<LevelKey>();
        FakeKey fakeKey = collision.GetComponent<FakeKey>();
        LevelArm levelArm = collision.GetComponent<LevelArm>();

        if (collision.CompareTag("Finish"))
        {
            _finischHintCanvas.SetActive(false);
            _isFinish = false;
            _pressECanvas.SetActive(false);
            chestAnimator.Play("CloseCh");
        }
        if (levelSwordTemp != null)
        {
            _pressECanvas.SetActive(false);
            _isLevelkey = false;
        }
        if (fakeKey != null)
        {
            _pressECanvas.SetActive(false);
            _isFakeKey = false;
        }
        if (levelArm != null)
        {
            _hintOpenLadderCanvas.SetActive(false);
            _pressECanvas.SetActive(false);
            _isLevelArm = false;
        }
    }
}
