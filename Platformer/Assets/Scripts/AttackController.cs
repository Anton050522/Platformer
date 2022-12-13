using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private bool _isAttack;

    private Animator animator;

    [SerializeField] private Weapon weapon;

    PlayerController _playerController;


    private void Start()
    {
        animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();   
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
            weapon.EnemyInRange(_playerController.IsFlip);
            animator.SetTrigger("attack");
        } 
    }

    public void FinishAttack()
    {
        _isAttack = false; 
       
    }
}
