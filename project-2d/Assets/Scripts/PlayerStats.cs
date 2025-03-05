using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int coins;
    public float health;
    public Vector2 playerPosition;

    private JohnMovement johnMovement;

    void Start()
    {
        johnMovement = GetComponent<JohnMovement>();

        if (johnMovement == null)
        {
            Debug.LogError("JohnMovement no encontrado en el objeto con PlayerStats.");
        }

        LoadGameData();
    }

    public GameData GetGameData()
    {
        if (johnMovement == null)
        {
            Debug.LogError("JohnMovement no está asignado en PlayerStats.");
            return null;
        }

        GameData data = new GameData
        {
            level = LevelManager.currentLevel,
            coins = johnMovement.Coins,
            health = johnMovement.Health,
            playerPosition = transform.position
        };
        return data;
    }

    public void LoadGameData()
    {
        GameData data = SaveSystem.LoadGame(1);
        if (data != null)
        {
            if (johnMovement != null)
            {
                johnMovement.Coins = data.coins;
                johnMovement.Health = (int)data.health;
            }
            transform.position = data.playerPosition;
        }
    }
}