using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    private bool _isActivated = false;

    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject _finischHintCanvas;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }    
    public void Activate()
    {
        _isActivated = true;    
    }

    public void FinishLevel()
    {
        if (_isActivated)
        {
            animator.Play("OpenCh");
            levelCompleteCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _finischHintCanvas.SetActive(true);
        }
    }
}
