using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public GameObject bulletPrefab;
    public float fireDelay = 0.5f;
    private bool isReadyToFire = true;

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire(Vector3 target)
    {
        if (isReadyToFire)
        {
            Vector3 direction = (target - transform.parent.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position + direction, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = direction;

            if (shootingSound)
            {
                shootingSound.Play();
            }

            Debug.Log("Bullet gun fired");

            // The gun is not ready to fire again yet
            isReadyToFire = false;

            // Start the coroutine to make the gun ready to fire again after the delay
            StartCoroutine(ReadyToFireAfterDelay());
        }
    }

    private IEnumerator ReadyToFireAfterDelay()
    {
        yield return new WaitForSeconds(fireDelay);

        isReadyToFire = true;
    }
}