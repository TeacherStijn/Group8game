using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Enemy user;
    public GameObject player;
    public abstract void Fire(GameObject target);

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }
}