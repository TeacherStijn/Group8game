using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected AudioSource shootingSound;

    public abstract void Fire(Vector3 target);

    protected virtual void Start()
    {
        shootingSound = GetComponent<AudioSource>();
    }
}