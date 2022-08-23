using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

[Serializable]
public class Hangman
{
    public string token;
    public string hangman;
}

public class Manager : MonoBehaviour
{
    private Hangman _hangman;

    public TextMeshProUGUI hangmanText;
    public TMP_InputField letterInputText;

    private void Awake()
    {
        hangmanText.gameObject.SetActive(false);
        letterInputText.gameObject.SetActive(false);
    }

    public void OnStartGameButtonPressed()
    {
        StartCoroutine(CreateGame_WebRequest());
    }

    public void OnLetterEntered()
    {
        Debug.Log(string.IsNullOrEmpty(letterInputText.text));
    }

    private IEnumerator CreateGame_WebRequest()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://hangman-api.herokuapp.com/hangman", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                _hangman = JsonUtility.FromJson<Hangman>(www.downloadHandler.text);

                hangmanText.gameObject.SetActive(true);
                letterInputText.gameObject.SetActive(true);

                hangmanText.text = _hangman.hangman.Replace('_', '□');
            }
        }
    }
}
