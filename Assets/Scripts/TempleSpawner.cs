using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject templePrefab;
    public int maxTemples = 6;

    public Vector2 minSpawnDistance = new Vector2(70, 70);
    public Vector2 maxSpawnDistance = new Vector2(140, 140);

    private void Start()
    {
        for (int i = 0; i < maxTemples; i++)
        {
            SpawnTemple();
        }
    }

    private void SpawnTemple()
    {
        // Randomly choose a direction (left/right and up/down).
        int directionX = Random.Range(0, 2) == 0 ? -1 : 1;
        int directionY = Random.Range(0, 2) == 0 ? -1 : 1;

        // Calculate a random distance within the allowed range.
        float distanceX = Random.Range(minSpawnDistance.x, maxSpawnDistance.x) * directionX;
        float distanceY = Random.Range(minSpawnDistance.y, maxSpawnDistance.y) * directionY;

        Vector3 spawnPos = new Vector3(distanceX, distanceY, 0);

        // Instantiate the temple.
        GameObject temple = Instantiate(templePrefab, spawnPos, Quaternion.identity);

        Debug.Log("Temple added");

        // Spawn guardian as well
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SpawnGuardian(temple);
    }
}
