using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyStats
{
    public float speed;
    public float detectionRadius;
    public GameObject player;

    public Weapon weapon;

    [SerializeField]
    public GameObject loot;

    private bool startShooting = false;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitWithShooting());
        if (weapon)
        {
            loot = weapon.gameObject;
        }
    }

    protected void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= detectionRadius && startShooting)
        {
            weapon.Fire(player.transform.position);
        }

        Move();
    }

    IEnumerator WaitWithShooting()
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

    public override void Die()
    {
        // semi random drop stuff for player?
        if (weapon || loot)
        {
            GameObject drop = Instantiate(loot, transform.position, Quaternion.identity);
            // Making bit bigger to see it
            drop.tag = "Loot";
            drop.transform.localScale *= 20;
            BoxCollider2D collider = drop.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            Debug.Log("Dropping some loot!");
        }

        base.Die();
    }
}
