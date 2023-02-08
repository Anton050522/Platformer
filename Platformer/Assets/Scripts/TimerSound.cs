using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSound : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    private float _timeToWait;
    private float _waitTime;

    private void Start()
    {
        _timeToWait = 11f;
        _waitTime = _timeToWait;
    }

    private void Update()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime < 0f)
        {
            _timeToWait = Random.Range(9f, 15f);
            _waitTime = _timeToWait;
            _sound.Play();
        }
    }
}
