using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    private float bestTime;
    
    [SerializeField] private Text FinishedTime;
    [SerializeField] private Text BestTime;

    public void ShowScreen(float finishedTime)
    {
        bestTime = Timer.getBestTime();

        float finishedTime = Timer.GetCurrentTime();
        if(IsNewBestTime(finishedTime))
        {
            bestTime = finishedTime;
            BestTime.text = "New Record! " + Timer.TimeToString(bestTime);
            FinishedTime.gameObject.SetActive(false);
            Timer.SaveTime(bestTime, SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FinishedTime.text = "Your Time: " + Timer.TimeToString(finishedTime);
            BestTime.text = "Current Record: " + Timer.TimeToString(bestTime);
        }
    }

        private bool IsNewBestTime(float newTime)
    {
        if(newTime < bestTime)
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
        SceneManager.LoadScene(Application.levelCount-1);
    }
}
