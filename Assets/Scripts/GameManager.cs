using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel, mainMenuPanel, gameDetailsPanel, pausePanel;
    public TextMeshProUGUI scoreText, highScoreText, coinsText, gameScoreText, gameCoinsText, gameTargetText, gameTimeText, muteText, gameOverText;

    EnvironmentManager environmentManager;
    SpawnManager spawnManager;

    private AudioSource playerAudio;
    public AudioClip mainMusic, gameMusic, gameSound, carHorn, coinSound;

    private bool isMuted, isGameOver, isGameActive, isGamePaused = false;

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

    public bool IsGamePaused
    {
        get { return isGamePaused; }
        set { isGamePaused = value; }
    }

    public bool HasGameStarted
    {
        get
        {
            return isGameActive && !isGameOver && !isGamePaused;
        }
    }

    public int Score
    {
        set
        {
            score = value;
            SetScore();
        }
    }

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        totalCoins = PlayerPrefs.GetInt("Coins", 0);
        isMuted = PlayerPrefs.GetInt("Mute", 0) != 0;

        highScoreText.text = $"High score: {highScore}";
        coinsText.text = $"Coins: {totalCoins}";

        SetMute();

        playerAudio = GetComponent<AudioSource>();

        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMainMusic();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameActive && !isGameOver && !isGamePaused && coins == target) 
        {
            Time.timeScale = 0;
            GameOver(true);
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        isGameOver = false;
        isGamePaused = false;

        mainMenuPanel.SetActive(false);
        gameDetailsPanel.SetActive(true);

        score = coins = 0;

        target = 10;
        gameTime = 60;

        SetScore();
        SetCoins();
        SetTarget();
        SetTime();

        environmentManager.BatchSpawnEnvironment();
        spawnManager.StartSpawn();
        InvokeRepeating("GameTimeRoutine", 0, 1);

        PlayGameMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        isGamePaused = true;
        pausePanel.SetActive(true);
        gameDetailsPanel.SetActive(false);
    }

    public void Resume()
    {
        isGamePaused = false;
        pausePanel.SetActive(false);
        gameDetailsPanel.SetActive(true);
    }

    public void Mute()
    {
        isMuted = !isMuted;
        int mute = isMuted ? 1 : 0;
        PlayerPrefs.SetInt("Mute", mute);
        SetMute();

        if (!isMuted) PlayMainMusic();
        else playerAudio.Stop();
    }

    public void EndGame()
    {
        isGameActive = false;
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        gameDetailsPanel.SetActive(false);
        pausePanel.SetActive(false);


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        PlayMainMusic();
    }

    public void GameOver(bool won)
    {
        if (won)
        {
            gameOverText.text = "You won!";
            gameOverText.color = Color.green;
        }
        else
        {
            gameOverText.text = "Game over!";
            gameOverText.color = Color.red;
        }

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

    void SetMute()
    {
        string muteStr = isMuted ? "Unmute" : "Mute";
        muteText.text = muteStr;
    }

    void GameTimeRoutine()
    {
        if (HasGameStarted)
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
        if (!isMuted)
        {
            playerAudio.PlayOneShot(coinSound);
        }
        coins++;
        SetCoins();
    }

    public void HitVehicle()
    {
        if (!isMuted)
        {
            playerAudio.PlayOneShot(carHorn);
        }
    }

    private void PlayMainMusic()
    {
        if (!isMuted) { 
            playerAudio.Stop();
            playerAudio.loop = true;
            playerAudio.clip = mainMusic;
            playerAudio.volume = 0.75f;
            playerAudio.Play();
        }
    }

    private void PlayGameMusic()
    {
        if (!isMuted)
        {
            playerAudio.Stop();
            playerAudio.loop = true;
            playerAudio.clip = gameMusic;
            playerAudio.volume = 0.75f;
            playerAudio.Play();

            //playerAudio.Stop();
            //playerAudio.loop = true;
            //playerAudio.clip = gameSound;
            //playerAudio.volume = 0.75f;
            //playerAudio.Play();
        }
    }
}
