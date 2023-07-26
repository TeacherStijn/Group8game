using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject rockPrefab;
    public float xSpawnRange = 175;
    public float ySpawnRange = 175;
    public int maxRocks = 200;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        for (int i = 0; i < maxRocks; i++)
        {
            SpawnRock();
        }
    }

    private void SpawnRock()
    {
        Vector3 spawnPosition;
        bool canSpawnHere = false;

        // Repeat until we find a position that is not occupied.
        while (!canSpawnHere)
        {
            float spawnPosX = Random.Range(mainCamera.transform.position.x - xSpawnRange, mainCamera.transform.position.x + xSpawnRange);
            float spawnPosY = Random.Range(mainCamera.transform.position.y - ySpawnRange, mainCamera.transform.position.y + ySpawnRange);
            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);

            canSpawnHere = !Physics2D.OverlapCircle(spawnPosition, 0.5f); // assuming the tree width is 1 unit

            if (canSpawnHere)
            {
                //Instantiate tree at the spawn position.
                Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
