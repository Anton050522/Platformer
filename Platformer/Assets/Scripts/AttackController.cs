using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private bool _isAttack;

    private Animator animator;

    [SerializeField] private Weapon weapon;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public bool IsAttack
    {
        get => _isAttack;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            _isAttack = true;
            weapon.EnemyInRange();
            animator.SetTrigger("attack");
        } 
    }

    public void FinishAttack()
    {
        _isAttack = false; 
       
    }
}
