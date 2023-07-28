using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public float xSpawnRange = 175;
    public float ySpawnRange = 175;
    public int maxTrees = 200;

    [Tooltip("Distance between the center of the spawned objects will be no lower than this")]
    public float minimumDistance = 1f;

    public Transform spawnedObjectContainer;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        for (int i = 0; i < maxTrees; i++)
        {
            SpawnTree();
        }
    }

    private void SpawnTree()
    {
        Vector3 spawnPosition;
        bool canSpawnHere = false;

        // Repeat until we find a position that is not occupied.
        while (!canSpawnHere)
        {
            float spawnPosX = Random.Range(mainCamera.transform.position.x - xSpawnRange, mainCamera.transform.position.x + xSpawnRange);
            float spawnPosY = Random.Range(mainCamera.transform.position.y - ySpawnRange, mainCamera.transform.position.y + ySpawnRange);
            spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);

            canSpawnHere = !Physics2D.OverlapCircle(spawnPosition, minimumDistance / 2);

            if (canSpawnHere)
            {
                //Instantiate tree at the spawn position.
                GameObject tree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
                if (spawnedObjectContainer)
                {
                    tree.transform.parent = spawnedObjectContainer;
                }
            }
        }
    }
}
