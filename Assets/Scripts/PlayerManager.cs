using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    private static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Player Manager instance already exists!");
        }

        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    #endregion

    public GameObject player;

    public static GameObject Player { get => instance.player; }
}
