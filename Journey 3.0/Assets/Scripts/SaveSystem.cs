using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "/PlayerProgression.saveFile";

    public static bool firstTimePlaying = true;
    
    public static void SaveProgress(ProgressionData progressionData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(stream, progressionData);
        
        stream.Close();
    }

    public static ProgressionData LoadProgressionData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            FileStream stream = new FileStream(path, FileMode.Open);

            ProgressionData progressionData = formatter.Deserialize(stream) as ProgressionData;
            
            stream.Close();

            return progressionData;
        }
        else
        {
            Debug.LogError("No save file found at: " + path);
            return null;
        }
    }
}

public static class LevelSelectSaveSystem
{
    public static string path = Application.persistentDataPath + "/GameComplete.saveFile";
    
    public static bool CheckGameFinished()
    {
        bool filePresent = File.Exists(path);

        return filePresent;
    }

    public static void CreateGameFinishedFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        FileStream stream = new FileStream(path, FileMode.Create);
        
        GameFinishedData file = new GameFinishedData(System.DateTime.Now.ToLongDateString());
        
        formatter.Serialize(stream, file);
        
        stream.Close();
    }
}

[System.Serializable]
public class ProgressionData
{
    //which section of the game is the player on
    public int saveSectionIndex;

    //is it night time in the current section
    public bool nightTime;

    public ProgressionData(int tempSaveSectionIndex, bool tempNightTime)
    {
        saveSectionIndex = tempSaveSectionIndex;
        nightTime = tempNightTime;
    }
}

[System.Serializable]
public class GameFinishedData
{
    public string completionDate;
    public GameFinishedData(string dateValue)
    {
        completionDate = dateValue;
    }
}
