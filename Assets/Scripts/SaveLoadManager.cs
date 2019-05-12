using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadManager
{
    private static string nameOfSaveFile = "/time_records.sav";

    public static void SaveTimes(float[] newBestTimes)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Create);
        
        TimeKeeper data = new TimeKeeper(newBestTimes);
        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public static float[] LoadTimes()
    {
        if (File.Exists(Application.persistentDataPath + nameOfSaveFile))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + nameOfSaveFile, FileMode.Open);
        
            TimeKeeper data = binaryFormatter.Deserialize(fileStream) as TimeKeeper;
            
            fileStream.Close();
            return data.GetTimes();
        } else {
            return ResetTimes();
        }
    }

    public static float[] ResetTimes()
    {
        float[] newArray = new float[SceneManager.sceneCountInBuildSettings - 1];

        for(int i = 0; i < newArray.Length; i++)
        {
                newArray[i] = float.MinValue;            
        }
        return newArray;
    }
}

[Serializable]
public class TimeKeeper 
{
    private float[] bestTimes;

    public TimeKeeper(float[] newBestTimes)
    {
        bestTimes = newBestTimes;
    }

    public float[] GetTimes()
    {
        return bestTimes;
    }
}
