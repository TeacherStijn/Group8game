using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TempleBossEnemy : Enemy
{
    public Transform startPoint;
    public float updateInterval = 1f;
    public float range = 7f;
    private Vector3 targetPosition;

    protected override void Start()
    {
        base.Start();
        this.detectionRadius = 3f;
        this.speed = 0.5f;
        startPoint = this.transform;
        InvokeRepeating("UpdateTarget", 0, updateInterval);
    }

    public override void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void UpdateTarget()
    {
        float randomX = Random.Range(startPoint.position.x - range, startPoint.position.x + range);
        float randomY = Random.Range(startPoint.position.y - range, startPoint.position.y + range);
        float randomZ = Random.Range(startPoint.position.z - range, startPoint.position.z + range);

        targetPosition = new Vector3(randomX, randomY, randomZ);
    }
}
