using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGun : Weapon
{
    [SerializeField]
    public GameObject magicPrefab;
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
            Vector2 direction = (target - user.position).normalized;

            GameObject magicBullet = Instantiate(magicPrefab, user.position + Vector3.up, Quaternion.identity);

            magicBullet.GetComponent<Rigidbody2D>().velocity = direction;

            Debug.Log("Magic gun fired");

            // The gun is not ready to fire again yet
            isReadyToFire = false;

            // Start the coroutine to make the gun ready to fire again after the delay
            StartCoroutine(ReadyToFireAfterDelay());
        }
    }

    private IEnumerator ReadyToFireAfterDelay()
    {
        yield return new WaitForSeconds(fireDelay);

        isReadyToFire = false;
    }
}