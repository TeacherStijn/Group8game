using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Holds all items in the inventory
    private List<GameObject> items = new List<GameObject>();
    private const int MAX_CRYSTALS = 4;
    public bool FoundCrystals = false;

    private void Update()
    {
        if (items != null)
        {
            int crystalCount = items.FindAll(obj => obj.CompareTag("Crystal")).Count;
            if (crystalCount >= MAX_CRYSTALS)
            {
                FoundCrystals = true;
            }
        }
    }

    // Adds an item to the inventory
    public void AddItem(GameObject item)
    {
        switch (item.tag) {
            case "Weapon": 
                items.Add(item);
                Debug.Log("Setting new weapon?");
                SetWeapon(item);
                break;
            case "Crystal":
                items.Add(item);
                break;
            default:
                Debug.Log("Picked up something else");
                break;
        }
    }

    public void SetWeapon(GameObject item)
    {
        /*        // Get Weapon slots container
                GameObject[] slots = GameObject.FindGameObjectWithTag("WeaponSlots").GetComponents<GameObject>();

                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i] == null)
                    {
                        slots[i] = Instantiate(item, transform, true);
                        return;
                    }
                }*/

        Transform parentTransform = GameObject.FindGameObjectWithTag("WeaponSlots").transform;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            GameObject slot = parentTransform.GetChild(i).gameObject;

            // Not sure if this is good;
            if (slot.transform.childCount == 0)
            {
                // Assign the item to the slot and break the loop
                item.transform.SetParent(slot.transform);
                item.transform.localPosition = Vector3.zero;
                return;
            }
        }

        // Add popup to load choose a spot to fill weapon 
        Debug.Log("Which slot to fill with weapon?");
    }

    // Removes an item from the inventory
    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
    }
}