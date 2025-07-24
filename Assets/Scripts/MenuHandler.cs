using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField nameField;
    public static string userName;

    private List<ScoreData> scoreData;

    // Start is called before the first frame update
    void Start()
    {
        scoreData = ScoreManager.Load();
        Debug.Log($"Загружено записей: {scoreData?.Count ?? 0}");

        if (scoreData.Count == 0)
        {
            Debug.LogWarning("Нет сохранённых данных");
        }
        else
        {
            ScoreData bestScore = scoreData.OrderByDescending(x => x.score).First();
            nameField.text = bestScore.playerName;
            scoreText.text = $"Best Score : {bestScore.playerName} : {bestScore.score}";
        }   
    }

    // Update is called once per frame
    void Update()
    {
        string inputText = nameField.text;
        userName = inputText;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("High Score");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
