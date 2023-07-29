using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class LaserGun : Weapon
{
    public GameObject laserPrefab;
    public float chargeTime = 2.0f;

    private bool isCharging = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire(Vector3 target)
    {
        if (isCharging)
        {
            return;
        }

        isCharging = true;

        // Simulate the enemy shaking while charging
        StartCoroutine(ChargeAndFire(target));
    }

    private IEnumerator ChargeAndFire(Vector3 target)
    {
        yield return new WaitForSeconds(chargeTime);

        Vector2 direction = (target - transform.position).normalized;

        GameObject laser = Instantiate(laserPrefab, transform.position + Vector3.up, Quaternion.identity);

        laser.GetComponent<Rigidbody2D>().velocity = direction;

        Debug.Log("Laser fired");
        
        yield return new WaitForSeconds(laser.GetComponent<Bullet>().lifetime);

        isCharging = false;
    }
}
