using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    private float bestTime;
    public Image crosshair;
    public Timer timer;
    [SerializeField] private Text FinishedTime;
    [SerializeField] private Text BestTime;

    public void ShowScreen()
    {
        bestTime = timer.GetBestTime();

        float finishedTime = timer.GetCurrentTime();
        if (IsNewBestTime(finishedTime))
        {
            bestTime = finishedTime;
            BestTime.text = "New Record! " + Timer.TimeToString(bestTime);
            FinishedTime.gameObject.SetActive(false);
            timer.SaveTime(bestTime, SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FinishedTime.text = "Your Time: " + Timer.TimeToString(finishedTime);
            BestTime.text = "Current Record: " + Timer.TimeToString(bestTime);
        }
    }

    private bool IsNewBestTime(float newTime)
    {
        if (newTime < bestTime)
        {
            return true;
        }
        return false;
    }

    public void PlayNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // after the last level, you go to the MainMenu
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        // At the moment the order is levels -> menu -> scene selection
        SceneManager.LoadScene(Application.levelCount - 2);
    }
}
