using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject gameDetailsPanel;
    public TextMeshProUGUI scoreText, highScoreText, coinsText, gameScoreText, gameCoinsText, gameTargetText, gameTimeText;

    private bool isGameOver, isGameActive = false;

    int score, coins, target, gameTime;

    int highScore;
    int totalCoins;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public bool IsGameActive
    {
        get { return isGameActive; }
        set { isGameActive = value; }
    }

    public int Score
    {
        set
        {
            score = value;
            SetScore();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalCoins = PlayerPrefs.GetInt("Coins", 0);

        highScoreText.text = $"High score: {highScore}";
        coinsText.text = $"Coins: {totalCoins}";

        score = coins = 0;
        target = 10;
        gameTime = 60;

        SetScore();
        SetCoins();
        SetTarget();
        SetTime();
        
        InvokeRepeating("GameTimeRoutine", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if (isGameOver)
        //{
        //    gameOverPanel.SetActive(true);
        //    scoreText.text = "You Scored " + score.ToString();
        //} //every other condition necessary for game over can be added here or refactored into a method

        if (isGameActive && !isGameOver && coins == target) 
        {
            GameOver(true);
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        mainMenuPanel.SetActive(false);
        gameDetailsPanel.SetActive(true);

        score = coins = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        PlayAgain();

        isGameActive = false;
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameDetailsPanel.SetActive(false);
    }

    void GameOver(bool won)
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameDetailsPanel.SetActive(false);
        scoreText.text = "You Scored " + score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        totalCoins += coins;
        PlayerPrefs.SetInt("Coins", totalCoins);
    }

    void SetScore()
    {
        gameScoreText.text = $"Score: {score}";
    }

    void SetCoins()
    {
        gameCoinsText.text = $"Coins: {coins}";
    }

    void SetTarget()
    {
        gameTargetText.text = $"Target: {target}";
    }

    void SetTime()
    {
        gameTimeText.text = $"Time Left: {gameTime}s";
    }

    void GameTimeRoutine()
    {
        if (isGameActive)
        {
            if (gameTime > 0)
            {
                SetTime();
                gameTime--;
            }
            else
            {
                SetTime();
                GameOver(false);
            }
        }
    }

    public void CollectCoin()
    {
        coins++;
        SetCoins();
    }
}
