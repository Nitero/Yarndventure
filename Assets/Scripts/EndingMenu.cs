using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenu : MonoBehaviour
{
    //Show Level 1 from Scenes In Build
    public void PlayGameAgain()
    {
        Transition.Start();
    }

    public void ExitGame()
    {
        Transition.Quit();
    }

    public void SceneSelection()
    {
        Transition.Next();
    }
}
