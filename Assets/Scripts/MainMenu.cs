using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Show Level 1 from Scenes In Build
    public void PlayGame()
    {
        Transition.Next();
    }

    public void TechDemo()
    {
        SceneManager.LoadScene(17);
    }

    public void ExitGame()
    {
        Transition.Quit();
    }
}