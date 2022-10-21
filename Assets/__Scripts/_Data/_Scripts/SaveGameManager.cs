using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class InformationsToSave
{
    public AchievementBase[] achievements; // achievements information
    public int highScore = 0;
    public int maxLevelReached = 0;
}

public static class SaveGameManager
{
    private static string path = $"{Application.persistentDataPath}/AsteraxSave.save";
    public static InformationsToSave informationsToSave;
    
    public static void SaveGame()
    {
        var jsonToSave = JsonUtility.ToJson(informationsToSave);
        File.WriteAllText(path, jsonToSave);
        Debug.Log("Data Saved in:" + path);
    }

    public static bool LoadSaveGame()
    {
        informationsToSave = HasSaveData() ? JsonUtility.FromJson<InformationsToSave>(File.ReadAllText(path)) : new InformationsToSave();
        Debug.Log("Data Loaded from: " + path);
        return File.Exists(path);
    }

    public static bool HasSaveData() => File.Exists(path);
    
    public static void DeleteSave()
    {
        if (HasSaveData())
        {
            File.Delete(path);
            informationsToSave = new InformationsToSave();
            foreach (var achievement in GameManager.Instance.GameSettings.Achievements)
            {
                achievement.completed = false;
            }
            Debug.Log("SAVE DELETED: " + path);
        }
    }
}
