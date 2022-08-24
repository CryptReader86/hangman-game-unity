using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Text;

[Serializable]
public class RandomWord
{
    public string word;
}

public class Manager : MonoBehaviour
{
    private string _wordToGuess;
    private string _currentWord;

    private int _numErrors;

    private List<string> _addedLetters = new List<string>();

    public TextMeshProUGUI hangmanText;
    public TMP_InputField letterInputText;
    public TextMeshProUGUI messageText;

    private void Awake()
    {
        hangmanText.gameObject.SetActive(false);
        letterInputText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

    public void OnStartGameButtonPressed()
    {
        StartCoroutine(GetRandomWord_WebRequest());
    }

    public void OnLetterEntered()
    {
        if(!string.IsNullOrEmpty(letterInputText.text))
        {
            bool isEnglishLetter = Regex.IsMatch(letterInputText.text, "[a-z]", RegexOptions.IgnoreCase);
            if (isEnglishLetter)
            {
                if(!_addedLetters.Contains(letterInputText.text))
                {
                    _addedLetters.Add(letterInputText.text);

                    messageText.text = "";

                    List<int> letterIndexes = new List<int>();

                    int nextIndex = 0;
                    do
                    {
                        nextIndex = _wordToGuess.IndexOf(letterInputText.text, nextIndex, _wordToGuess.Length - nextIndex);

                        if (nextIndex != -1)
                        {
                            letterIndexes.Add(nextIndex);

                            nextIndex++;
                        }
                    }
                    while (nextIndex >= 0 && nextIndex < _wordToGuess.Length);

                    if (letterIndexes.Count > 0)
                    {
                        foreach (int index in letterIndexes)
                        {
                            StringBuilder sb = new StringBuilder(_currentWord);
                            sb[index] = char.Parse(letterInputText.text);
                            _currentWord = sb.ToString();
                        }

                        hangmanText.text = _currentWord;

                        if(_currentWord == _wordToGuess)
                        {
                            Debug.Log("You win!!!");
                        }
                    }
                    else
                    {
                        _numErrors++;
                        if(_numErrors >= 7)
                        {
                            Debug.Log("You lose!!!");
                        }
                    }
                }
                else
                {
                    messageText.color = Color.red;
                    messageText.text = "LETTER ALREADY ADDED";
                }
            }
            else
            {
                messageText.color = Color.red;
                messageText.text = "NOT A VALID LETTER";
            }
        }

        letterInputText.text = "";
    }

    private IEnumerator GetRandomWord_WebRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://api.api-ninjas.com/v1/randomword"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                _wordToGuess = JsonUtility.FromJson<RandomWord>(www.downloadHandler.text).word;
                _currentWord = new string('□', _wordToGuess.Length);

                _numErrors = 0;

                _addedLetters.Clear();

                hangmanText.gameObject.SetActive(true);
                letterInputText.gameObject.SetActive(true);
                messageText.gameObject.SetActive(true);

                hangmanText.text = _currentWord;
                messageText.text = "";
                letterInputText.text = "";

                Debug.Log("word to guess: " + _wordToGuess);
            }
        }
    }
}
