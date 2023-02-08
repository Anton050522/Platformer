using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private bool _isAttack;

    [SerializeField] private Animator _atackAnimator;

    [SerializeField] private Weapon weapon;
    [SerializeField] private AudioSource _atackSound;

    PlayerController _playerController;

    private void Start()
    {
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
            _atackAnimator.SetTrigger("attack");
            _atackSound.Play();
        } 
    }

    public void FinishAttack()
    {
        _isAttack = false; 
    }
}
