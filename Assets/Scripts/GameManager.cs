using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button quitBtn, playAgainBtn;
    bool gameOver = false;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        quitBtn.onClick.AddListener(QuitGame);
        playAgainBtn.onClick.AddListener(PlayAgain);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            scoreText.text = "You Scored " + score.ToString();
        }//every other condition necessary for game over can be added here or refactored into a method
    }
    void QuitGame()
    {
        Application.Quit();
    }

    void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Score(int finalScore)
    {
        score = finalScore;
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
