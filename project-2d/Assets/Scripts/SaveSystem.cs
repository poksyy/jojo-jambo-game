using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save";

    public static void SaveGame(GameData data, int slot)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath + slot + ".json", json);
        Debug.Log("Game saved in slot " + slot);
    }

    public static GameData LoadGame(int slot)
    {
        string path = savePath + slot + ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded from slot " + slot);
            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in slot " + slot);
            return null;
        }
    }
}