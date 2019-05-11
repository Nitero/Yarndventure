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
    

    public void ShowScreen()
    {
        FinishedTime.text = "Your Time: " + timer.TimeToString(timer.getCurrentTime());
        FinishedTime.gameObject.SetActive(true);
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