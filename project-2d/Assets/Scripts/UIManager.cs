using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Button level1Button;
    public Button level2Button;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Level1Completed", 0) == 1)
        {
            UnlockLevel2();
        }
        else
        {
            LockLevel2();
        }
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void CompleteLevel1()
    {
        PlayerPrefs.SetInt("Level1Completed", 1);
        PlayerPrefs.Save();

        UnlockLevel2();
    }

    private void LockLevel2()
    {
        if (level2Button != null)
        {
            level2Button.interactable = false;
        }
    }

    private void UnlockLevel2()
    {
        if (level2Button != null)
        {
            level2Button.interactable = true;
        }
    }
}
