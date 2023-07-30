using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    //public Text timerText;
    private float startTime;
    private bool stop = false;

    void Start()
    {
        startTime = Time.time;
        // StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (!stop)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            GetComponent<TextMeshPro>().text = minutes + ":" + seconds;

            yield return null;
        }
    }

    public void StopTimer()
    {
        stop = true;
    }
}