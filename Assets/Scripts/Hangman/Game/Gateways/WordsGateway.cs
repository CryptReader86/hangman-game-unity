using UniRx;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;

namespace Hangman.Game.Gateways
{
    public class WordsGateway : IWordsGateway
    {
        private ReactiveProperty<string> _randomWord;
        private ReactiveProperty<string> _error;

        private string _serviceUrl;

        public IReadOnlyReactiveProperty<string> RandomWord { get; private set; }
        public IReadOnlyReactiveProperty<string> Error { get; private set; }

        public WordsGateway(string serviceUrl)
        {
            _serviceUrl = serviceUrl;

            _randomWord = new ReactiveProperty<string>();
            RandomWord = new ReadOnlyReactiveProperty<string>(_randomWord);

            _error = new ReactiveProperty<string>();
            Error = new ReadOnlyReactiveProperty<string>(_error);
        }

        public void GetRandomWord()
        {
            ObservableWWW.Get(_serviceUrl).Subscribe(OnSuccess, OnError);
        }

        private void OnSuccess(string result)
        {
            try
            {
                _randomWord.Value = JsonUtility.FromJson<Word>(result).word;
            }
            catch(Exception e)
            {
                OnError(e);
            }
        }

        private void OnError(Exception exception)
        {
            _error.Value = exception.ToString();
        }

        [Serializable]
        private class Word
        {
            public string word;
        }
    }
}
