using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Contains logic of all UI elements in the level scenes
//
// The visibility of the UI elements and their updates is managed in this class.
// It contains the gameplay UI elements (timer and cross hair) and the menu
// with the score after clearing the level.
public class LevelUI : MonoBehaviour
{
    // Gameplay UI
    [SerializeField] private GameObject gameplay;
    // ScoreMenu UI
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private Text FinishedTime;
    [SerializeField] private Text BestTime;

    // Shows gameplay UI and hides score menu
    public void SelectGamePlayUI()
    {
        gameplay.SetActive(true);
        levelMenu.SetActive(false);
    }

    // Shows score menu and hides gameplay UI
    public void SelectScoreMenuUI()
    {
        levelMenu.SetActive(true);
        gameplay.SetActive(false);
    }

    public void SetScoreUI(float finishedTime, float bestTime) {
        FinishedTime.text = "Your Time: " + Timer.TimeToString(finishedTime);
        BestTime.text = "Current Record: " + Timer.TimeToString(bestTime);
    }

    public void SetScoreUIWithNewRecord(float bestTime) {
        BestTime.text = "New Record! " + Timer.TimeToString(bestTime);
        FinishedTime.gameObject.SetActive(false);
    }

    // Next Level Button onclick function
    public void PlayNextLevel()
    {
        // after the last level, you go to the MainMenu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Play Again Button onclick function
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Go to Menu Button onclick function
    public void GoToMenu()
    {
        // At the moment the order is levels -> menu -> scene selection
        SceneManager.LoadScene(Application.levelCount - 2);
    }
}