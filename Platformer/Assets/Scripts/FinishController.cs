using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    private bool _isActivated = false;

    Animator animator;

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
            //gameObject.SetActive(false);
            animator.Play("OpenCh");
        }
        else
        {
            Debug.Log("Найдите ключ");
        }
    }
}
