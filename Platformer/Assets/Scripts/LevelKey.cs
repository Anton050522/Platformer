using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private FinishController finish;

    [SerializeField] private GameObject _hintFinishKeyCanvas;

    private SpriteRenderer _spriteKey;

    private float _timeToWait;
    private float _waitTime;

    private bool _hint = false;
    private void Start()
    {
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishController> ();
        _spriteKey = GetComponent<SpriteRenderer>();
        _timeToWait = 3f;
        _waitTime = _timeToWait;
    }
    private void Update()
    {
        if (_hint)
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime < 0)
            {
                _waitTime = _timeToWait;
                _hintFinishKeyCanvas.SetActive(false);
                gameObject.SetActive(false);
                _hint = false;
            }
        }
    }
    public void ActivateLevelKey()
    {
        _hintFinishKeyCanvas.SetActive(true);
        finish.Activate();
        Debug.Log("Это ключ от финиша");
        _hint = true;
        _spriteKey.enabled = false;
    }
}
