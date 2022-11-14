using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class InformationsToSave
{
    public int highScore = 0;
    public int maxLevelReached = 0;
    public int currentBodyIndex = 0; // 0 == default body
    public int currentTurretIndex = 0; // 0 == default turret
}

public static class SaveGameManager
{
    private static string path = $"{Application.persistentDataPath}/AsteraxSave.save";
    public static InformationsToSave informationsToSave = new InformationsToSave();
    
    public static void SaveGame()
    {
        var jsonToSave = JsonUtility.ToJson(informationsToSave);
        File.WriteAllText(path, jsonToSave);
        Debug.Log("Data Saved in:" + path);
    }

    public static bool LoadSaveGame()
    {
        informationsToSave = HasSaveData() ? JsonUtility.FromJson<InformationsToSave>(File.ReadAllText(path)) : new InformationsToSave();
        return HasSaveData();
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
        }
    }
}
