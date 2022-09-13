using NUnit.Framework;
using NSubstitute;
using Hangman.Game.Entities;
using Hangman.Game.UseCases;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Tests.UseCases
{
    public class StartGameUseCaseShould
    {
        private IHangman _hangman;
        private IWordsGateway _wordsGateway;
        private StartGameUseCase _startGameUseCase;

        [SetUp]
        public void Setup()
        {
            _hangman = Substitute.For<IHangman>();
            _wordsGateway = Substitute.For<IWordsGateway>();
            _startGameUseCase = new StartGameUseCase(_hangman, _wordsGateway);
        }

        [Test]
        public void Reset_The_Hangman_Model()
        {
            WhenInvoking();

            ThenHangmanModelIsReseted();
        }

        [Test]
        public void Try_To_Get_A_Random_Word()
        {
            WhenInvoking();

            ThenGetRandomWordIsCalled();
        }

        private void WhenInvoking()
        {
            _startGameUseCase.Execute();
        }

        private void ThenHangmanModelIsReseted()
        {
            _hangman.Received(1).Reset();
        }

        private void ThenGetRandomWordIsCalled()
        {
            _wordsGateway.Received(1).GetRandomWord();
        }
    }
}
