using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            animator.SetTrigger("isAttack");
            playerHealth.ReduceHealth(_damage);

        }
    }
}
