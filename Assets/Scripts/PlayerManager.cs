using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    public GameObject summoningSpot;
    private InventoryManager inventoryManager;

    private void Start()
    {
        summoningSpot = GameObject.FindGameObjectWithTag("SummoningSpot");
        inventoryManager = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        CheckForSummoningSpot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) {
            case "Crystal":
                Debug.Log("Yes! You got a crystal");
                // Gain the item + log something + achievement
                inventoryManager.AddItem(collision.gameObject);
                break;

            case "Loot":
                Debug.Log("Got a new gun!");
                inventoryManager.AddItem(collision.gameObject);
                break;
                
            default:
                break;
        }
    }

    private void CheckForSummoningSpot()
    {
        float distanceToSummoningSpot = Vector3.Distance(transform.position, summoningSpot.transform.position);
        // Distance threshold 
        if (distanceToSummoningSpot < 2f)
        {
            Debug.Log("You are near the summoning spot!");
            
            if (GetComponent<InventoryManager>().FoundCrystals)
            {
                Debug.Log("You have gathered all crystals!");
                // Start quest text
                // Spawn Dragon
                GameObject dragonPrefab = GameObject.FindGameObjectWithTag("FinalDragon");
                GameObject theDragon = Instantiate(dragonPrefab, summoningSpot.transform, true);

                // Setting Questmarker to Parent Dragon
                theDragon.transform.SetParent(GameObject.Find("Summoning Marker").transform);
            }
        }
    }
}