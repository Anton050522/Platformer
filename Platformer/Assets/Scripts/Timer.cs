using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Animator _animator;
    private float _timeToWait;
    private float _waitTime;

    private void Start()
    {
        _timeToWait = 4.5f;
        _waitTime = _timeToWait;
        _animator= GetComponent<Animator>();    
    }

    private void Update()
    {
        _waitTime -= Time.deltaTime;
        _timeToWait = Random.Range(4f, 9f);
        if (_waitTime < 0)
        {
            _waitTime = _timeToWait;
            _animator.SetTrigger("play");
        }
    }
}
