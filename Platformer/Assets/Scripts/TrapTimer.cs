using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTimer : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _damage = 20f;

    private float _timeToWait;
    private float _waitTime;

    private bool _isDamage = false;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        _timeToWait = Random.Range(4f, 9f); ;
        _waitTime = _timeToWait;
        boxCollider2D = GetComponent<BoxCollider2D>();  
    }

    private void Update()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0)
        {
            _waitTime = _timeToWait;
            _animator.SetTrigger("attack");
            _sound.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamage)
        {
            playerHealth.ReduceHealth(_damage);
            _isDamage = false;
        }
    }

    public void FinishAttack()
    {
        _isDamage = false;
       boxCollider2D.enabled = false;
    }

    public void StartDamage()
    {
        _isDamage = true;   
        boxCollider2D.enabled= true;    
    }





}
