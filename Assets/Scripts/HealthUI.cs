using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    private PlayerStats stats;
    private TMP_Text healthText;

    private void Start()
    {
        healthText = GetComponent<TMP_Text>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        stats.onHealthChanged += UpdateHealthUI;
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }
}
