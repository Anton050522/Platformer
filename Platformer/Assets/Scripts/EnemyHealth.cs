using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float _health = 100f;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("takeDamage");
        if (_health <= 0f)
        {
            
           Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        

    }
}
