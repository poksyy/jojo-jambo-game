using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.UnlockNextLevel();

            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                SaveSystem.SaveGame(playerStats.GetGameData(), 1);
            }

            Debug.Log("Level Complete! Next level unlocked.");
        }
    }
}