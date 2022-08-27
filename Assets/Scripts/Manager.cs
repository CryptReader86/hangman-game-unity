using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Text.RegularExpressions;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class RandomWord
{
    public string word;
}

public class Manager : MonoBehaviour
{
    private string _wordToGuess;
    private string _currentWord;

    private StringBuilder _addedLettersStringBuilder = new StringBuilder();

    private int _numErrors;

    private List<string> _addedLetters = new List<string>();

    public TextMeshProUGUI hangmanText;
    public TMP_InputField letterInputText;
    public TextMeshProUGUI addedLettersText;

    public Image[] hangImages;

    private void Awake()
    {
        hangmanText.gameObject.SetActive(false);
        letterInputText.gameObject.SetActive(false);
        addedLettersText.gameObject.SetActive(false);

        SetCurrentHangImage();
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
                            letterInputText.gameObject.SetActive(false);

                            _addedLettersStringBuilder.Append("<color=\"black\">" + letterInputText.text + "</color>");
                        }
                        else
                        {
                            _addedLettersStringBuilder.Append("<color=\"black\">" + letterInputText.text + "</color>, ");
                        }

                        addedLettersText.text = _addedLettersStringBuilder.ToString();
                    }
                    else
                    {
                        _numErrors++;

                        SetCurrentHangImage();

                        if (_numErrors >= 7)
                        {
                            letterInputText.gameObject.SetActive(false);

                            _addedLettersStringBuilder.Append("<color=\"red\">" + letterInputText.text + "</color>");
                        }
                        else
                        {
                            _addedLettersStringBuilder.Append("<color=\"red\">" + letterInputText.text + "</color>, ");
                        }

                        addedLettersText.text = _addedLettersStringBuilder.ToString();
                    }
                }
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
                addedLettersText.gameObject.SetActive(true);

                hangmanText.text = _currentWord;
                addedLettersText.text = "";
                letterInputText.text = "";

                _addedLettersStringBuilder.Clear();

                SetCurrentHangImage();

                Debug.Log("word to guess: " + _wordToGuess);
            }
        }
    }

    private void SetCurrentHangImage()
    {
        for(int i = 0; i < hangImages.Length; i++)
        {
            hangImages[i].gameObject.SetActive(i == _numErrors);
        }
    }
}
