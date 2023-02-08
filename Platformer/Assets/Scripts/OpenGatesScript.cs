using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGatesScript : MonoBehaviour
{
    [SerializeField] private Animator _animatorGates;
    [SerializeField] private Animator _animatorLadder;
    private AudioSource _audio;

    private float _timeToWait;
    private float _waitTime;

    private bool _isOpen = false;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _timeToWait = 15f; 
        _waitTime = _timeToWait;
    }

    private void Update()
    {
        if (_isOpen)
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime < 0)
            {
                _waitTime = _timeToWait;
                _animatorGates.SetTrigger("close");
                _animatorLadder.SetTrigger("close");
                _audio.Play();
                _isOpen = false;
            }
        }
    }

    public void BoomObject()
    {
        _animatorGates.SetTrigger("open");
        _animatorLadder.SetTrigger("open");
        _audio.Play();  
        _isOpen= true;
    }
}
