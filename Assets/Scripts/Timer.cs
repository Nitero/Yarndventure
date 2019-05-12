using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    private float startTime;
    private float timeCounter;

    // Start is called before the first frame update
    private void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        timeCounter = Time.time - startTime;
        timerText.text = TimeToString(timeCounter);
    }

    public static string TimeToString(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)time % 60;
        float miliseconds = time - (int)time;

        return minutes.ToString() + ":" + (seconds.ToString("00")) + ":" + (miliseconds * 1000).ToString("000");
    }

    public float GetCurrentTime()
    {
        return timeCounter;
    }

    public void ResetTime()
    {
        startTime = Time.time;
    }

    public void StopTimer()
    {
        timerText.gameObject.SetActive(false);
    }

    public void Finish()
    {
        timerText.color = Color.yellow;
    }

    // This method is unfinished!
    // Intention: checking how many scenes contain "level"
    // Reason: making sure, if future scenes get added without being a level, no error occurs
    // Problem: SceneManager.GetSceneByBuildIndex(int buildIndex).name only works for current (or probably loaded) scene
    private void CountScenes()
    {
        // string name = SceneManager.GetActiveScene().name;
        // print (name);
        // print(Application.levelCount);
        // print(SceneManager.GetSceneByBuildIndex(1).name);
        // int numberOfLevels;

        // for (int i = 0; i < Application.levelCount; i++)
        //     {
        //         if(SceneManager.GetSceneByBuildIndex(2).name)
        //     }
    }
}
