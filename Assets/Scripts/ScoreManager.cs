using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ScoreManager
{
    private static string fileName = "data2.json";

    public static void Save(List<ScoreData> data)
    {
        var wrapper = new ScoreDataList { data = data };
        string json = JsonUtility.ToJson(wrapper, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
        Debug.Log("Сохранено в: " + path);
    }

    public static List<ScoreData> Load()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var wrapper = JsonUtility.FromJson<ScoreDataList>(json);
            return wrapper?.data ?? new List<ScoreData>();
        }
        else
        {
            Debug.LogWarning("Файл не найден. Возвращаем новые данные.");
            return new List<ScoreData>();
        }
    }
}

[Serializable]
public class ScoreData
{
    public string playerName;
    public int score;

    // Сохраняем дату как строку без времени
    public string date;

    public long time;

    // Удобное свойство для доступа
    public DateTime Date
    {
        get => DateTime.ParseExact(date, "yyyy-MM-dd", null);
        set => date = value.ToString("yyyy-MM-dd");
    }
}

[Serializable]
public class ScoreDataList
{
    public List<ScoreData> data;
}
