using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private Text FinishedTime;
    [SerializeField] private Text BestTime;
    private float bestTime = float.MaxValue;

    public void ShowScreen(float finishedTime)
    {
        if (IsNewBestTime(finishedTime))
        {
            bestTime = finishedTime;
            BestTime.text = "New Record! " + Timer.TimeToString(bestTime);
            FinishedTime.gameObject.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}