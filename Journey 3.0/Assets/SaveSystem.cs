using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/player.progress";
    
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

[System.Serializable]
public class ProgressionData
{
    public int saveSectionIndex;

    public bool nightTime;

    public ProgressionData(int tempSaveSectionIndex, bool tempNightTime)
    {
        saveSectionIndex = tempSaveSectionIndex;

        nightTime = tempNightTime;
    }
}
