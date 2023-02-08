using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyModelTransfor;

    [SerializeField] private float _walkDistance = 6f;
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private float _timeToChase = 2f;
    [SerializeField] private float _chasingSpeed = 3f;
    [SerializeField] Animator _animator;

    private float _waitTime;
    private float _timeToWait;    
    private float _chaseTime;
    private float _walkSpeed;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChanginPlayer;
    private bool _collidedWithPlayer;

    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private Vector2 _leftBoundaryPositin;
    private Vector2 _rightBounfaryPosition;
    private Vector2 _nextPoint;

    public bool IsFacingRight
    {
        get => _isFacingRight;
    }

    public void StartChanginPlayer()
    {
        _isChanginPlayer = true;
        _chaseTime = _timeToChase;
        _walkSpeed = _chasingSpeed;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _leftBoundaryPositin = transform.position;
        _rightBounfaryPosition = _leftBoundaryPositin + Vector2.right * _walkDistance; //Vector2.right = new Vector2(1, 0)

        _timeToWait = Random.Range(1f, 5f);
        _waitTime = _timeToWait;
        _chaseTime = _timeToChase;
        _walkSpeed = _patrolSpeed;
    }

    private void Update()
    {
        if (_isChanginPlayer)
        {
            StartChasingTime();
        }
        if (_isWait && !_isChanginPlayer)
        {
            StartWaitTimer();
            
        }
        if (ShouldWait())
        {
            _isWait = true;

            _animator.SetTrigger("idle");
        }
    }

    private void FixedUpdate()
    {
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if (_isChanginPlayer && _collidedWithPlayer)
        {
            return;
        }

        if (_isChanginPlayer)
        {
            ChasePlayer();
        }

        if (!_isWait && !_isChanginPlayer)
        {
            Patrol();
        }
    }

    private float DistaceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;

    }
    private void Patrol()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
        _animator.Play("Enemy_run");
    }

    private void ChasePlayer()
    {
        float distance = DistaceToPlayer();
        if (distance < 0)
        {
            _nextPoint.x *= -1;
        }

        if (distance > 0.5f && !_isFacingRight)
        {
            Flip();
        }
        else if (distance < 0.5f && _isFacingRight)
        {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
        _animator.Play("Enemy_run");
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransfor.localScale;
        playerScale.x *= -1;
        enemyModelTransfor.localScale = playerScale;
    }
    private void StartWaitTimer()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime = _timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void StartChasingTime()
    {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime < 0f)
        {
            _chaseTime = _timeToChase;
            _isChanginPlayer = false;
            _walkSpeed = _patrolSpeed;
        }
    }

    private bool ShouldWait()
    {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBounfaryPosition.x;
        bool isOutLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPositin.x;

        //_animator.SetTrigger("idle");
        return isOutOfRightBoundary || isOutLeftBoundary;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPositin, _rightBounfaryPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            _collidedWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            _collidedWithPlayer = false;
        }
    }
}
