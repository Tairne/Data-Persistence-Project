using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreHandler : MonoBehaviour
{
    public GameObject rowPrefab; // Префаб строки
    public Transform container; // Контейнер для строк (Content)

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current time: " + DateTime.Now);
        List<ScoreData> entries = ScoreManager.Load();
        entries.Sort((a, b) => b.score.CompareTo(a.score)); // Сортировка по убыванию
        foreach (var entry in entries)
        {
            var row = Instantiate(rowPrefab, container);
            var texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = entry.playerName;
            texts[1].text = entry.date.ToString();
            texts[2].text = entry.time.ToString();
            texts[3].text = entry.score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
