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

    // Shows clear time of current level and the current best time
    public void SetScoreUI(float finishedTime, float bestTime)
    {
        FinishedTime.text = "Your Time: " + Timer.TimeToString(finishedTime);
        BestTime.text = "Current Record: " + Timer.TimeToString(bestTime);
    }

    // Shows the new record
    public void SetScoreUIWithNewRecord(float bestTime)
    {
        BestTime.text = "New Record! " + Timer.TimeToString(bestTime);
        FinishedTime.gameObject.SetActive(false);
    }

    // Next Level Button onclick function
    public void PlayNextLevel()
    {
        Transition.Next();
    }

    // Play Again Button onclick function
    public void PlayAgain()
    {
        Transition.Reload();
    }

    // Go to Menu Button onclick function
    public void GoToMenu()
    {
        Transition.Start();
    }

    public void PrepareTechDemo()
    {
        levelMenu.SetActive(false);

        Component[] components;
        components = gameplay.GetComponentsInChildren<Component>();

        foreach (Component component in components)
        {
            string componentName = component.gameObject.ToString();
            if(componentName.Contains("Crosshair"))
            {                
                component.gameObject.SetActive(false);
            }            
        }        
    }
}