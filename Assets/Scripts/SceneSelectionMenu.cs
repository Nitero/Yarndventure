using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSelectionMenu : MonoBehaviour
{
    //Show Level 1 from Scenes In Build
    public void Level1()
    {
        LoadLevel(1);
    }

    //Show Level 2 from Scenes In Build
    public void Level2()
    {
        LoadLevel(2);
    }

    //Show Level 3 from Scenes In Build
    public void Level3()
    {
        LoadLevel(3);
    }

    //Show Level 4 from Scenes In Build
    public void Level4()
    {
        LoadLevel(4);
    }

    //Show Level 5 from Scenes In Build
    public void Level5()
    {
        LoadLevel(5);
    }

    //Show Level 6 from Scenes In Build
    public void Level6()
    {
        LoadLevel(6);
    }

    //Show Level 7 from Scenes In Build
    public void Level7()
    {
        LoadLevel(7);
    }

    //Show Level 8 from Scenes In Build
    public void Level8()
    {
        LoadLevel(8);
    }

    //Show Level 9 from Scenes In Build
    public void Level9()
    {
        LoadLevel(9);
    }

    //Show Level 10 from Scenes In Build
    public void Level10()
    {
        LoadLevel(10);
    }

    //Show Level 10 from Scenes In Build
    public void Level11()
    {
        LoadLevel(11);
    }

    // Determines which level scene is loaded
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level - 1);
    }
}
