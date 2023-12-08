using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public Image healthBarImage;
    private void Awake()
    {

    }
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            // Handle player death or any other logic when health reaches zero
            Debug.Log("Player is dead!");
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }
}