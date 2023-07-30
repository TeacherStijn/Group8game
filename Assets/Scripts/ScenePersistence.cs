using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistence : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
