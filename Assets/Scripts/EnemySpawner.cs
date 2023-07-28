using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] guardianPrefabs;
    public float spawnRate = 2f;
    public int maxEnemies = 150;

    // Not spawn enemies right away; giving player some time
    public float spawnDelay = 3f;
    public Transform spawnedObjectContainer;

    private float nextSpawnTime;
    private int currentEnemies = 0;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        nextSpawnTime = Time.time + spawnRate + spawnDelay;
    }

    private void Update()
    {
        if (ShouldSpawn())
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private bool ShouldSpawn()
    {
        return Time.time >= nextSpawnTime && currentEnemies < maxEnemies;
    }

    private void SpawnEnemy()
    {
        nextSpawnTime = Time.time + spawnRate;

        int prefabIndex = Random.Range(0, enemyPrefabs.Length);

        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        //float levelH = GameObject.Find("LevelBackground").transform.position.y;
        //float levelW = GameObject.Find("LevelBackground").transform.position.x;

        Vector3 spawnPos;
        bool canSpawnHere = false;
        int attempts = 0;

        do
        {
            float spawnPosX = Random.Range(-camWidth / 2f - 1, camWidth / 2f);  // The "+ 1" makes the enemy is spawning outside of the camera's view.
            float spawnPosY = Random.Range(-camHeight / 2f - 1, camHeight / 2f);

            //float spawnPosX = Random.Range(-levelW / 2f - 1, levelW / 2f);  // The "+ 1" makes the enemy is spawning outside of the camera's view.
            //float spawnPosY = Random.Range(-levelH / 2f - 1, levelH / 2f);

            // Determine which side of the screen to spawn the enemy on.
            int spawnSide = Random.Range(0, 4);
            switch (spawnSide)
            {
                case 0: // Top
                    spawnPosY = camHeight / 2f + 1;
                    break;
                case 1: // Bottom
                    spawnPosY = -camHeight / 2f - 1;
                    break;
                case 2: // Right
                    spawnPosX = camWidth / 2f + 1;
                    break;
                case 3: // Left
                    spawnPosX = -camWidth / 2f - 1;
                    break;
            }

            spawnPos = new Vector3(mainCamera.transform.position.x + spawnPosX, mainCamera.transform.position.y + spawnPosY, 0);

            // change 1f to the radius size you wish to check around spawnPos.
            canSpawnHere = !Physics2D.OverlapCircle(spawnPos, 1f); 
            attempts++;

        } while (!canSpawnHere && attempts < 100); // Try 100 times to spawn the enemy in a free space.

        if (canSpawnHere)
        {
            GameObject enemy = Instantiate(enemyPrefabs[prefabIndex], spawnPos, Quaternion.identity);
            AssignWeaponToEnemy(enemy.GetComponent<Enemy>());
            currentEnemies++;
            Enemy.OnDestroyed -= () => currentEnemies--;
            if (spawnedObjectContainer)
            {
                enemy.transform.parent = spawnedObjectContainer;
            }
        }
    }

    public void SpawnGuardian(GameObject location)
    {
        Transform spawnPos = location.transform;

        // Hard coded one single guardian for now!
        GameObject guardian = Instantiate(guardianPrefabs[0], spawnPos.position, Quaternion.identity);
        guardian.transform.parent = spawnedObjectContainer;
        AssignWeaponToEnemy(guardian.GetComponent<Enemy>());
    }

    private void AssignWeaponToEnemy(Enemy enemy)
    {
        Transform weaponHolder = enemy.transform.Find("WeaponHolder");
        if (weaponHolder == null)
        {
            weaponHolder = new GameObject("WeaponHolder").transform;
            weaponHolder.transform.parent = enemy.transform;

            // right edge of the enemy sprite
            weaponHolder.transform.localPosition = new Vector3(0.5f, 0, 0); 
        }

        enemy.weapon = Instantiate(enemy.weapon, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        enemy.weapon.user = enemy.transform;
    }

    // Nog even cleanup:
    private void OnDestroy()
    {
        Enemy.OnDestroyed -= () => currentEnemies--;
    }
}

