using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    private static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Player Manager instance already exists!");
        }

        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #endregion

    public GameObject player;

    public static GameObject Player { get => instance.player; }

    public Vector3 summoningSpot = Vector3.zero;
    public GameObject dragon;
    public GameObject summonMarker;
    public TMP_Text relicUI;
    public int crystalCount = 0;

    private void Start()
    {
    }

    static public void AddWeapon(Weapon weapon)
    {
        Debug.Log("Add Weapon");

        var playerControls = Player.GetComponent<PlayerControls>();

        var referenceWeapon = playerControls.weapons[playerControls.weaponCount % playerControls.weaponSlotCount + playerControls.weaponCount - playerControls.weaponSlotCount];
        playerControls.weapons.Insert(0, Instantiate(weapon, referenceWeapon.transform.position, referenceWeapon.transform.rotation, referenceWeapon.transform.parent));

        playerControls.weaponCount++;

    }

    static public void AddCrystal()
    {
        instance.relicUI.text = "Relics Collected: " + ++instance.crystalCount + "/4";

        if (instance.crystalCount >= 4)
        {
            Debug.Log("You have gathered all crystals!");
            instance.relicUI.text = "Return to summoning area";

            // Spawn Dragon
            instance.dragon.SetActive(true);
            instance.summonMarker.SetActive(true);
        }

    }

    static public void RestoreHealth(float restoreValue)
    {
        Player.GetComponent<PlayerStats>().RestoreHealth(restoreValue);
    }
}