using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   [SerializeField] private float totalHealth = 100f;
   [SerializeField] private Slider healthSlider;
   [SerializeField] private GameObject gameObjectCanvas;

   [SerializeField] private AudioSource _hitSound;

    private float _health;
    [SerializeField] private Animator _playerHitAnimator;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }
    public void ReduceHealth(float damage)
    {
        _hitSound.Play();
        _health -= damage;
        InitHealth();
        _playerHitAnimator.SetTrigger("takeDamage");
        if (_health <= 0f)
        {
            Die();
        }
    }
    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        gameObjectCanvas.SetActive(true);  
    }
}
