using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerListManager : MonoBehaviour
{
    public Transform playerListContainer;
    public TMP_Text responseText;
    public TMP_FontAsset thaleahFatFont;

    private string getPlayersUrl = "http://localhost/api/get_users.php";

    void Start()
    {
        StartCoroutine(GetPlayers());
    }

    IEnumerator GetPlayers()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(getPlayersUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server Response: " + webRequest.downloadHandler.text);

                try
                {
                    PlayerListResponse playerListResponse = JsonUtility.FromJson<PlayerListResponse>(webRequest.downloadHandler.text);

                    if (playerListResponse != null && playerListResponse.players != null)
                    {
                        foreach (var player in playerListResponse.players)
                        {
                            GameObject playerItem = new GameObject("PlayerItem", typeof(RectTransform));
                            playerItem.transform.SetParent(playerListContainer, false);

                            LayoutElement layout = playerItem.AddComponent<LayoutElement>();
                            layout.preferredHeight = 20;

                            TextMeshProUGUI textComponent = playerItem.AddComponent<TextMeshProUGUI>();
                            textComponent.text = player.username;
                            textComponent.fontSize = 20;
                            textComponent.color = Color.black;
                            textComponent.alignment = TextAlignmentOptions.Left;
                            textComponent.enableAutoSizing = false;
                            textComponent.enableWordWrapping = true;
                            textComponent.font = thaleahFatFont;

                            RectTransform rt = playerItem.GetComponent<RectTransform>();
                            rt.sizeDelta = new Vector2(120, 40);
                            rt.anchorMin = new Vector2(0, 1);
                            rt.anchorMax = new Vector2(1, 1);
                            rt.anchoredPosition = new Vector2(0, 0);
                        }
                    }
                    else
                    {
                        responseText.text = "No players found.";
                    }
                }
                catch (System.Exception ex)
                {
                    responseText.text = "Error parsing players: " + ex.Message;
                }
            }
            else
            {
                responseText.text = "Error: " + webRequest.error;
            }
        }
    }
}

[System.Serializable]
public class Player
{
    public string username;
    public string email;
}

[System.Serializable]
public class PlayerListResponse
{
    public Player[] players;
}
