using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public float detectionRadius;
    public GameObject player;

    public Weapon weapon;

    [SerializeField]
    public GameObject loot;

    public delegate void EnemyDestroyed();
    public static event EnemyDestroyed OnDestroyed;

    private bool startShooting = false;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.position);
        StartCoroutine(StartShooting());
        Debug.Log("***************** Coroutine done");
    }

    protected void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= detectionRadius && startShooting)
        {
            Debug.Log("--------------- FIRING! -------------");
            Debug.Log("Wie is het? " + player.transform.position);
            weapon.Fire(player);
        }

        Move();
    }

    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(2); // wait for 3 seconds
        startShooting = true;
    }

    public virtual void Move()
    {
        // empty move pattern
    }

    // When firing a heavy weapon the enemy starts shaking
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDestroyed?.Invoke();

            // semi random drop stuff for player?
            if (loot)
            {
                Vector3 spawnPos = transform.position;
                Instantiate(loot, spawnPos, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
