using UnityEngine;
using UnityEngine.SceneManagement;

public class Score
{
    private float bestTime;  // of current level
    private float[] bestTimes;

    public Score()
    {
        bestTimes = SaveLoadManager.LoadTimes();
        bestTime = bestTimes[SceneManager.GetActiveScene().buildIndex];
        // printTimes();
    }

    // Return true if a new high score was set, else false.
    public bool UpdateScore(float finishedTime)
    {
        if (IsNewBestTime(finishedTime))
        {
            bestTime = finishedTime;
            bestTimes[SceneManager.GetActiveScene().buildIndex] = bestTime;
            SaveLoadManager.SaveTimes(bestTimes);
            return true;
        }
        return false;
    }

    private bool IsNewBestTime(float newTime)
    {
        if (newTime < bestTime)
        {
            return true;
        }
        return false;
    }

    public float GetBestTime()
    {
        return bestTime;
    }

    private void printTimes()
    {
        for (int i = 0; i < bestTimes.Length; i++)
        {
            Debug.Log(i + ". " + bestTimes[i]);
        }
    }
}
