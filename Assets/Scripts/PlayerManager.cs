using System.Collections;
using System.Collections.Generic;
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

    public GameObject summoningSpot;

    private void Start()
    {
        summoningSpot = GameObject.FindGameObjectWithTag("SummoningSpot");
    }

    private void Update()
    {
        CheckForSummoningSpot();
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