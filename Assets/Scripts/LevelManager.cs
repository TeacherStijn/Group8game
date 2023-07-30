using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
        else
        {
            Debug.Log("No scene left to load");
        }
    }

    public static void RetryFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
