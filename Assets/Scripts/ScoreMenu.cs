using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
    private float bestTime;
    
    [SerializeField]
    public Image crosshair;
    public Timer timer;
    public Text FinishedTime;
    public Text BestTime;
    
    public void ShowScreen()
    {
        bestTime = timer.getBestTime();

        float finishedTime = timer.GetCurrentTime();
        if(IsNewBestTime(finishedTime)) 
        {            
            bestTime = finishedTime;
            BestTime.text = "New Record! " + timer.TimeToString(bestTime);
            FinishedTime.gameObject.SetActive(false);
            timer.SaveTime(bestTime, SceneManager.GetActiveScene().buildIndex);
        } 
        else 
        {
            FinishedTime.text = "Your Time: " + timer.TimeToString(finishedTime);
            BestTime.text = "Current Record: " + timer.TimeToString(bestTime);                 
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