using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalLoot : Loot
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Yes! You got a crystal");
        // Gain the item + log something + achievement
    }
}
