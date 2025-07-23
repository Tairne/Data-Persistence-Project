using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField nameField;
    public static string userName;

    private ScoreData scoreData;

    // Start is called before the first frame update
    void Start()
    {
        scoreData = ScoreManager.Load();
        nameField.text = scoreData.playerName;
        scoreText.text = $"Best Score : {scoreData.playerName} : {scoreData.score}";
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

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
