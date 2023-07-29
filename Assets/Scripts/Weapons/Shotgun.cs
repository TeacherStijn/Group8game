using System.Collections;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public GameObject bulletPrefab;
    public int bulletCount = 5; // Number of bullets in the spread
    public float spreadAngle = 30f; // The angle of spread in degrees
    public float fireDelay = 0.5f;
    private bool isReadyToFire = true;

    protected override void Start()
    {
        base.Start();
    }

    public override void Fire(Vector3 target)
    {
        if (!isReadyToFire)
        {
            return;
        }

        Vector3 direction = (target - transform.parent.position).normalized;

        // Calculate the initial rotation angle for the spread
        float initialAngle = - spreadAngle / 2f;
        float angleStep = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            //Quaternion rotation = Quaternion.Euler(0f, 0f, initialAngle + i * angleStep);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0f, 0f, initialAngle + i * angleStep);

            GameObject bullet = Instantiate(bulletPrefab, transform.position + direction, rotation);

            // Calculate the direction for the current bullet based on the rotation
            Vector2 bulletDirection = new Vector2(Mathf.Cos(rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(rotation.eulerAngles.z * Mathf.Deg2Rad));
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection;
        }

        // The gun is not ready to fire again yet
        isReadyToFire = false;
        StartCoroutine(ReadyToFireAfterDelay());
    }

    private IEnumerator ReadyToFireAfterDelay()
    {
        yield return new WaitForSeconds(fireDelay);

        isReadyToFire = true;
    }
}