using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class LaserGun : Weapon
{
    [SerializeField]
    public GameObject laserPrefab;
    public float chargeTime = 2.0f;
    public float laserDuration = 1.0f;
    public float laserSpeed = 5f;

    private bool isCharging = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire(GameObject target)
    {
        if (isCharging || !target)
        {
            return;
        }

        isCharging = true;

        // Simulate the enemy shaking while charging
        StartCoroutine(ChargeAndFire());
    }

    private IEnumerator ChargeAndFire()
    {
        // Shake enemy for chargeTime
        user.StartCoroutine(user.Shake(2f, 0.1f));

        yield return new WaitForSeconds(chargeTime);

        // Instantiate the laser at the top of the enemy
        GameObject laser = Instantiate(laserPrefab, user.transform.position + Vector3.up, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;

        Debug.Log("Laser fired");

        yield return new WaitForSeconds(laserDuration);

        Destroy(laser);
        isCharging = false;
    }
}
