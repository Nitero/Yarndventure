using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField]
    public Timer timer;
    public Image crosshair;
    public Text FinishedTime;
    public Text BestTime;
    private float bestTime = float.MaxValue;
    
    public void ShowScreen()
    {
        float finishedTime = timer.getCurrentTime();
        if(IsNewBestTime(finishedTime)) 
        {            
            bestTime = finishedTime;            
            BestTime.text = "New Record! " + timer.TimeToString(bestTime);            
            FinishedTime.gameObject.SetActive(false);
        } else {
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