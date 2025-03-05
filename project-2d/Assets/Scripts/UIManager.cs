using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        loadButton.onClick.AddListener(LoadGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    void LoadGame()
    {
        SceneManager.LoadScene("SlotSelect");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}