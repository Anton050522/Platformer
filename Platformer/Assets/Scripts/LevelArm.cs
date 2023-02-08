using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArm : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audio;
    private OpenGatesScript destroyObject;

    private float _timeToWait;
    private float _waitTime;

    private bool _isOpen = false;

    [SerializeField] private GameObject _game;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
        _audio= GetComponent<AudioSource>();
        destroyObject = FindObjectOfType<OpenGatesScript>();

        _timeToWait = 10f;
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
                _animator.SetTrigger("close");
                _audio.Play();
                _isOpen = false;
            }
        }
    }
    public void ActivateLevelArm()
    {
        Debug.Log("Вы что-то включили");
        _game.SetActive(true);
        _animator.SetTrigger("activate");
        _audio.Play();
        destroyObject.BoomObject();
        _isOpen= true;
    }
}
