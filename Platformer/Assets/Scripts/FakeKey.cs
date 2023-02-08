using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKey : MonoBehaviour
{
    [SerializeField] private GameObject _hintDestroyKeyCanvas;

    private float _timeToWait;
    private float _waitTime;

    private SpriteRenderer _spriteKey;

    private bool _hint = false;

    private void Start()
    {
        _timeToWait = 3f;
        _waitTime = _timeToWait;
        _spriteKey = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_hint)
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime < 0)
            {
                _waitTime = _timeToWait;
                _hintDestroyKeyCanvas.SetActive(false);
                gameObject.SetActive(false);
                _hint = false;
            }
        }
    }
    public void ActivateFakeKey()
    {
        _hint = true; 
        _hintDestroyKeyCanvas.SetActive(true);
        _spriteKey.enabled = false;
        Debug.Log("Это сломанный ключ");
        
    }
}
