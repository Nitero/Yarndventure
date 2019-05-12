using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectionMenu : MonoBehaviour
{

    [SerializeField] private Text timeLevel01;
    [SerializeField] private Text timeLevel02;
    [SerializeField] private Text timeLevel03;
    [SerializeField] private Text timeLevel04;
    [SerializeField] private Text timeLevel05;
    [SerializeField] private Text timeLevel06;
    [SerializeField] private Text timeLevel07;
    [SerializeField] private Text timeLevel08;
    [SerializeField] private Text timeLevel09;
    [SerializeField] private Text timeLevel10;
    
    private float[] bestTimes;

    void Start()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        ShowTimeRecords();
    }

    private void ShowTimeRecords()
    {
        timeLevel01.text = Timer.TimeToString(bestTimes[0]);
        timeLevel02.text = Timer.TimeToString(bestTimes[1]);
        timeLevel03.text = Timer.TimeToString(bestTimes[2]);
        timeLevel04.text = Timer.TimeToString(bestTimes[3]);
        timeLevel05.text = Timer.TimeToString(bestTimes[4]);
        timeLevel06.text = Timer.TimeToString(bestTimes[5]);
        timeLevel07.text = Timer.TimeToString(bestTimes[6]);
        timeLevel08.text = Timer.TimeToString(bestTimes[7]);
        timeLevel09.text = Timer.TimeToString(bestTimes[8]);
        timeLevel10.text = Timer.TimeToString(bestTimes[9]);
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
