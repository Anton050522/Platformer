using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio= GetComponent<AudioSource>();
    }
    private void Update()
    {
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.E))
        {
            _audio.Play(); 
            OnDeactivate();  
        }
    }
    private void OnDeactivate()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
