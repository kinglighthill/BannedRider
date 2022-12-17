using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    public Button startBtn, optionsBtn, quitBtn;
    [Header("Panel")]
    public GameObject menuPanel, optionPanel;
    [Header("Options")]
    public AudioSource audioS;
    public Button backBtn;
    public Slider volSlide;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        optionsBtn.onClick.AddListener(OpenOptions);
        backBtn.onClick.AddListener(CloseOptions);
        volSlide.onValueChanged.AddListener(UpdateVolume);
    }

    // Update is called once per frame
    void OpenOptions()
    {
        optionPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    void CloseOptions()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    void UpdateVolume(float val)
    {
        val = volSlide.value;
        audioS.volume = val;
    }

    void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
