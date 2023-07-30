using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLoot : Loot
{
    public float rotationSpeed = 50f;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed);
    }
}
