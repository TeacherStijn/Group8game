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
        int crystalCount = items.FindAll(obj => obj.CompareTag("Crystal")).Count;
        if (crystalCount >= MAX_CRYSTALS)
        {
            FoundCrystals = true;
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
        // Get Weapon slots container
        GameObject[] slots = GameObject.Find("Weapon slots").GetComponents<GameObject>();
        
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = Instantiate(item, transform, true);
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