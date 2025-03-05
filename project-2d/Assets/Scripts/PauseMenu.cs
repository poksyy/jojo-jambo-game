using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button saveButton;
    public Button quitButton;

    private PlayerStats playerStats;

    void Start()
    {
        pausePanel.SetActive(false);
        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(ResumeGame);
        saveButton.onClick.AddListener(SaveGame);
        quitButton.onClick.AddListener(QuitGame);

        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void TogglePause()
    {
        bool isPaused = !pausePanel.activeSelf;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        if (playerStats != null)
        {
            GameData data = playerStats.GetGameData();

            SaveSystem.SaveGame(data, 1);

            Debug.Log("Partida guardada en el slot 1.");
        }
        else
        {
            Debug.LogError("PlayerStats no encontrado. No se pudo guardar la partida.");
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}