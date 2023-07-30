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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("") && !collision.gameObject.CompareTag(null))
        {
            if (collision.gameObject.CompareTag("Crystal"))
            {
                Debug.Log("Found a new crystal!!");
                inventoryManager.AddItem(this.gameObject);
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
