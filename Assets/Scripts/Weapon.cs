using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Fire(Vector3 target);

    protected virtual void Start()
    {
    }
}