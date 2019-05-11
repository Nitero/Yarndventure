using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float timeCounter;

    // Start is called before the first  frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.time - startTime;
        timerText.text = TimeToString(timeCounter);
    }

    public string TimeToString(float time)
    {
        int minutes = (int) (time/60);
        int seconds = (int) time % 60;
        float miliseconds = time - (int) time;

        return minutes.ToString() + ":" + (seconds.ToString("00")) + ":" + (miliseconds * 1000).ToString("000");
    }

    public float GetCurrentTime()
    {
        return timeCounter;
    }

    public void ResetTime()
    {
        startTime = Time.time;
    }

    public void StopTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    public void Finish()
    {
        timerText.color = Color.yellow;
    }
}