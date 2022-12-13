using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;
    [SerializeField] private float _timeToDamage = 3f;

    private float _damageTime;
    private bool _isDamage = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _damageTime = _timeToDamage;
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _damageTime -= Time.deltaTime;
            if (_damageTime < 0)
            {
                _isDamage = true;
                _damageTime = _timeToDamage;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamage)
        {
            //animator.SetTrigger("isAttack");
            playerHealth.ReduceHealth(_damage);
            _isDamage = false;

        }
    }
}
