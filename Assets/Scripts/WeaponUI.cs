using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    private PlayerStats stats;
    private PlayerControls controls;
    private TMP_Text weaponText;

    private void Start()
    {
        weaponText = GetComponent<TMP_Text>();
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        stats.onHealthChanged += UpdateWeaponUI;
    }

    public void UpdateWeaponUI(float currentHealth, float maxHealth)
    {
        weaponText.text = "Weapons active: " + System.Math.Min(controls.weaponSlotCount, controls.weaponCount - controls.brokenWeaponCount) + "/" + controls.weaponSlotCount;
    }
}
