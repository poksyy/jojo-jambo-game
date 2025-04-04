    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    [System.Serializable]
    public class ServerResponse
    {
        public string message;
        public int status;
    }

    public class UserRegistration : MonoBehaviour
    {
        public TMP_InputField usernameInput;
        public TMP_InputField emailInput;
        public Text responseText;
        public Button registerButton;

        private string postUrl = "http://localhost/api/save_user.php";

        void Start()
        {
            registerButton.onClick.AddListener(RegisterUser);
        }

        public void RegisterUser()
        {
            if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(emailInput.text))
            {
                responseText.text = "Error: Please fill in all fields.";
                return;
            }

            StartCoroutine(SendUserData());
        }

        IEnumerator SendUserData()
        {
            string jsonData = "{\"username\":\"" + usernameInput.text + "\", \"email\":\"" + emailInput.text + "\"}";

            using (UnityWebRequest webRequest = new UnityWebRequest(postUrl, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");

                yield return webRequest.SendWebRequest();

                Debug.Log("Server Response: " + webRequest.downloadHandler.text);

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string response = webRequest.downloadHandler.text;

                    if (response.Contains("message") && response.Contains("status"))
                    {
                        ServerResponse serverResponse = JsonUtility.FromJson<ServerResponse>(response);

                        if (serverResponse.status == 1)
                        {
                            SceneManager.LoadScene("MainMenu");
                        }
                        else
                        {
                            responseText.text = "Error: " + serverResponse.message;
                        }
                    }
                    else
                    {
                        responseText.text = "Unexpected response: " + response;
                    }
                }
                else
                {
                    responseText.text = "Error: " + webRequest.error;
                }
            }
        }
    }
