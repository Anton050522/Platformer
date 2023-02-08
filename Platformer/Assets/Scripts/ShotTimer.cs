using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTimer : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    //[SerializeField] private Animator _animator1;

    private float _timeToWait;
    private float _waitTime;

    private Animator _animator;

    private void Start()
    {
        _timeToWait = 5f;
        _waitTime = _timeToWait;
        _animator = GetComponent<Animator>();   
    }

    private void Update()
    {
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _timeToWait = Random.Range(5f, 9f);
            _waitTime = _timeToWait;
            _sound.Play();
            _animator.SetTrigger("isAttack");
            //_animator1.SetTrigger("isAttack");
        }
    }
}
