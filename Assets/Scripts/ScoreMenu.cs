using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour {
    [SerializeField]
    public Timer timer;
    public Text FinishedTime;

    public void showScreen () {
        FinishedTime.text = timer.getTime ();
        FinishedTime.gameObject.SetActive (true);
        timer.stopTimer ();
    }

    public void PlayNextLevel () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }

    public void PlayAgain () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
    }

    public void GoToMenu () {
        SceneManager.LoadScene (0);
    }


}