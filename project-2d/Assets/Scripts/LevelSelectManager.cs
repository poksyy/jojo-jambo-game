using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button backButton;

    void Start()
    {
        // Verificar el estado de los niveles al iniciar
        CheckLevelStatus();

        // Asignar listeners a los botones
        level1Button.onClick.AddListener(() => LoadLevel(1));
        level2Button.onClick.AddListener(() => LoadLevel(2));
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    void CheckLevelStatus()
    {
        // Cargar datos guardados para verificar el progreso
        GameData data = SaveSystem.LoadGame(1); // Cargar del slot 1 (puedes cambiar el slot si es necesario)

        // Verificar si el nivel 2 está desbloqueado
        if (data != null && data.level >= 2)
        {
            UnlockLevel2();
        }
        else
        {
            LockLevel2();
        }
    }

    void LoadLevel(int levelNumber)
    {
        // Cargar el nivel seleccionado
        if (levelNumber == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (levelNumber == 2 && LevelManager.currentLevel >= 2)
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            Debug.Log("Level 2 is locked. Complete Level 1 to unlock it.");
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void UnlockLevel2()
    {
        if (level2Button != null)
        {
            level2Button.interactable = true;
        }
    }

    void LockLevel2()
    {
        if (level2Button != null)
        {
            level2Button.interactable = false;
        }
    }
}