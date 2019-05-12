using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectionMenu : MonoBehaviour
{
    private float[] bestTimes;
    [SerializeField] private GameObject timeRecords;

    private void Start()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        ShowTimeRecords();
    }

    // Shows time records below their corresponding level
    private void ShowTimeRecords()
    {
        Text[] records = timeRecords.GetComponentsInChildren<Text>();
        Debug.Assert(records.Length <= bestTimes.Length);

        for (int i = 0; i < records.Length; i++)
        {
            float score = bestTimes[i];
            if (score > 0.0f)
            {
                records[i].text = Timer.TimeToString(score);
            }
            else
            {
                records[i].text = "No Time Record";
            }
        }
    }

    //Show Level 1 from Scenes In Build
    public void Level01()
    {
        LoadLevel(1);
    }

    //Show Level 2 from Scenes In Build
    public void Level02()
    {
        LoadLevel(2);
    }

    //Show Level 3 from Scenes In Build
    public void Level03()
    {
        LoadLevel(3);
    }

    //Show Level 4 from Scenes In Build
    public void Level04()
    {
        LoadLevel(4);
    }

    //Show Level 5 from Scenes In Build
    public void Level05()
    {
        LoadLevel(5);
    }

    //Show Level 6 from Scenes In Build
    public void Level06()
    {
        LoadLevel(6);
    }

    //Show Level 7 from Scenes In Build
    public void Level07()
    {
        LoadLevel(7);
    }

    //Show Level 8 from Scenes In Build
    public void Level08()
    {
        LoadLevel(8);
    }

    //Show Level 9 from Scenes In Build
    public void Level09()
    {
        LoadLevel(9);
    }

    //Show Level 10 from Scenes In Build
    public void Level10()
    {
        LoadLevel(10);
    }

    //Show Level 11 from Scenes In Build
    public void Level11()
    {
        LoadLevel(11);
    }

    // Determines which level scene is loaded
    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
