using UnityEngine.TestTools;
using System.Collections;
using UnityEngine;
using NUnit.Framework;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Tests.Gateways
{

    public class WordsGatewayShould
    {
        private bool _requestSent;
        private bool _serverResponse;

        private string _randomWord;
        private string _error;

        private WordsGateway _wordsGateway;

        static string[] TestGoodServiceUrls = new string[] { "https://api.api-ninjas.com/v1/randomword" };
        static string[] TestBadServiceUrls = new string[] { "https://api.api-ninjas.com/v1/error", "https://www.google.com/" };

        [SetUp]
        public void SetUp()
        {
            _requestSent = false;
            _serverResponse = false;

            _randomWord = null;
            _error = null;
        }

        [UnityTest]
        public IEnumerator Return_A_Random_Word_On_Success_Response([ValueSource("TestGoodServiceUrls")] string serviceUrl)
        {
            GivenAWordsGatewayWithSubscriptions(serviceUrl);

            WhenTryingToGetARandomWord();

            while (!_serverResponse)
                yield return null;

            ThenGotARandomWord();
        }

        [UnityTest]
        public IEnumerator Return_An_Error_On_Error_Response([ValueSource("TestBadServiceUrls")] string serviceUrl)
        {
            GivenAWordsGatewayWithSubscriptions(serviceUrl);

            WhenTryingToGetARandomWord();

            while (!_serverResponse)
                yield return null;

            ThenGotAnError();
        }

        private void GivenAWordsGatewayWithSubscriptions(string serviceUrl)
        {
            _wordsGateway = new WordsGateway(serviceUrl);

            _wordsGateway.RandomWord.Subscribe(randomWord =>
            {
                if (!_requestSent)
                    return;

                _randomWord = randomWord;
                _serverResponse = true;
            });
            _wordsGateway.Error.Subscribe(error =>
            {
                if (!_requestSent)
                    return;

                _error = error;
                _serverResponse = true;
            });
        }

        private void WhenTryingToGetARandomWord()
        {
            _wordsGateway.GetRandomWord();

            _requestSent = true;
        }

        private void ThenGotARandomWord()
        {
            Assert.IsNotNull(_randomWord);
        }

        private void ThenGotAnError()
        {
            Assert.IsNotNull(_error);
        }
    }
}
