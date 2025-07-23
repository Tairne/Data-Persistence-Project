using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ScoreManager
{
    private static string fileName = "scoredata.json";

    public static void Save(ScoreData data)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log("Сохранено в: " + path);
    }

    public static ScoreData Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("Файл не найден. Возвращаем новые данные.");
            return new ScoreData { playerName = "", score = 0};
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}
