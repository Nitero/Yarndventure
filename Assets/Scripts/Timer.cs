using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float startTime;
    private float timeCounter;
    private float[] bestTimes;
    private float bestTime; // of current level

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        bestTimes = SaveLoadManager.LoadTimes();
        bestTime = bestTimes[SceneManager.GetActiveScene().buildIndex];
        // printTimes();
    }

    private void printTimes()
    {
        for (int i = 0; i < bestTimes.Length; i++)
        {
            print(i + ". " + bestTimes[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = Time.time - startTime;
        timerText.text = TimeToString(timeCounter);
    }

    public static string TimeToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        float miliseconds = time - (int)time;

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

    public void SaveTime(float newBestTime, int buildIndex)
    {
        bestTimes[buildIndex] = newBestTime;
        SaveLoadManager.SaveTimes(bestTimes);
    }

    public float GetBestTime()
    {
        return bestTime;
    }

    public void Finish()
    {
        timerText.color = Color.yellow;
    }

    // This method is unfinished!
    // Intention: checking how many scenes contain "level"
    // Reason: making sure, if future scenes get added without being a level, no error occurs
    // Problem: SceneManager.GetSceneByBuildIndex(int buildIndex).name only works for current (or probably loaded) scene
    private void CountScenes()
    {
        // string name = SceneManager.GetActiveScene().name;
        // print (name);
        // print(Application.levelCount);
        // print(SceneManager.GetSceneByBuildIndex(1).name);
        // int numberOfLevels;

        // for (int i = 0; i < Application.levelCount; i++)
        //     {
        //         if(SceneManager.GetSceneByBuildIndex(2).name)
        //     }
    }
}
