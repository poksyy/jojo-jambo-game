using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour
{
    public Button slot1Button;
    public Button slot2Button;
    public Button backButton;

    void Start()
    {
        CheckSavedGames();

        slot1Button.onClick.AddListener(() => LoadSlot(1));
        slot2Button.onClick.AddListener(() => LoadSlot(2));
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    void CheckSavedGames()
    {
        GameData slot1Data = SaveSystem.LoadGame(1);
        GameData slot2Data = SaveSystem.LoadGame(2);

        slot1Button.interactable = (slot1Data != null);
        slot2Button.interactable = (slot2Data != null);
    }

    void LoadSlot(int slot)
    {
        GameData data = SaveSystem.LoadGame(slot);

        if (data != null)
        {
            LevelManager.currentLevel = data.level;

            SceneManager.LoadScene("Level" + data.level);
        }
        else
        {
            Debug.Log("No hay partida guardada en el slot " + slot);
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}