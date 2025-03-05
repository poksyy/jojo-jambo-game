using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel = 1;

    public static void UnlockNextLevel()
    {
        if (currentLevel < 2)
        {
            currentLevel++;
            Debug.Log("Level " + currentLevel + " unlocked!");
        }
    }
}