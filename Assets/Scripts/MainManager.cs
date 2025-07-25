using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private List<ScoreData> scoreData;
    private ScoreData bestScore;
    private float elapsedTime = 0f;
    private int secondsPassed = 0;


    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        scoreData = ScoreManager.Load();
        Debug.Log($"��������� �������: {scoreData?.Count ?? 0}");

        if (scoreData.Count == 0)
        {
            Debug.LogWarning("��� ����������� ������");
        }
        else
        {
            bestScore = scoreData.OrderByDescending(x => x.score).First();
            bestScoreText.text = $"Best Score : {bestScore.playerName} : {bestScore.score}";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (m_Started && !m_GameOver)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 1f)
            {
                int secondsToAdd = Mathf.FloorToInt(elapsedTime);
                secondsPassed += secondsToAdd;
                elapsedTime -= secondsToAdd;
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        List<string> existNames = scoreData.Select(x => x.playerName).ToList();

        if (existNames.Contains(MenuHandler.userName))
        {
            ScoreData targetRow = scoreData.Where(x => x.playerName == MenuHandler.userName).FirstOrDefault();
            if (targetRow.score < m_Points)
            {
                targetRow.score = m_Points;
                targetRow.Date = System.DateTime.Now;
                targetRow.time = secondsPassed;
                ScoreManager.Save(scoreData);
            }  
        }
        else
        {
            scoreData.Add(new ScoreData
            {
                playerName = MenuHandler.userName,
                score = m_Points,
                Date = System.DateTime.Now,
                time = secondsPassed
            });

            ScoreManager.Save(scoreData);
        }
    }
}
