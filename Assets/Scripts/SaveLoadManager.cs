﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

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
            return data.getTimes();
        } else {
            return ResetTimes();
        }
    }

    public static float[] ResetTimes()
    {
        float[] newArray = new float[Application.levelCount - 1];

        for(int i = 0; i < newArray.Length; i++)
        {
                newArray[i] = float.MaxValue;            
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

    public float[] getTimes()
    {
        return bestTimes;
    }

}