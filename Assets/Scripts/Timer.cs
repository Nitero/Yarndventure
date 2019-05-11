using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private int minuteCount;
    private float secondsCount; // it includes the miliseconds as well
    private float miliseconds;

    // Start is called before the first  frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        secondsCount = Time.time - startTime;
        miliseconds = secondsCount - (int)secondsCount;

        if (secondsCount >= 60)
        {
            minuteCount++;
            startTime = Time.time;
        }

        timerText.text = GetTime();
    }

    public string GetTime()
    {
        return minuteCount.ToString() + ":" + ((int)secondsCount).ToString("00") + ":" + (miliseconds * 1000).ToString("000");
    }

    public void ResetTime()
    {
        startTime = Time.time;
        minuteCount = 0;
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