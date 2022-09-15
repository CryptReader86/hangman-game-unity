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
        private IHangmanGame _hangmanGame;
        private IWordsGateway _wordsGateway;
        private StubStartGameUseCase _startGameUseCase;
        private ReactiveProperty<string> _randomWordProperty;
        private ReactiveProperty<string> _errorProperty;

        private const string TestRandomWord = "blablabla";
        private const string TestError = "Test Error";

        [Test]
        public void Reset_The_Hangman_Game()
        {
            GivenAHangmanGame();
            GivenAWordsGateway();
            GivenAStartGameUseCase();

            WhenInvoking();

            ThenHangmanModelIsReseted();
        }

        [Test]
        public void Try_To_Get_A_Random_Word()
        {
            GivenAHangmanGame();
            GivenAWordsGateway();
            GivenAStartGameUseCase();

            WhenInvoking();

            ThenGetRandomWordIsCalled();
        }

        [Test]
        public void Get_A_Random_Word_When_There_Is_A_Success_Response()
        {
            GivenAHangmanGame();
            GivenAWordsGatewayThatReturnsARandomWord();
            GivenAStartGameUseCase();

            WhenInvokingAndSendingARandomWord(TestRandomWord);

            ThenARandomWordWasReceived();
        }

        [Test]
        public void Get_An_Error_When_There_Is_An_Error_Report()
        {
            GivenAHangmanGame();
            GivenAWordsGatewayThatReturnsAnError();
            GivenAStartGameUseCase();

            WhenInvokingAndSendingAnError(TestError);

            ThenAnErrorWasReceived();
        }

        private void GivenAHangmanGame()
        {
            _hangmanGame = Substitute.For<IHangmanGame>();
        }

        private void GivenAWordsGateway()
        {
            _wordsGateway = Substitute.For<IWordsGateway>();
        }

        private void GivenAWordsGatewayThatReturnsARandomWord()
        {
            GivenAWordsGateway();

            _randomWordProperty = new ReactiveProperty<string>();

            _wordsGateway.RandomWord.Returns(new ReadOnlyReactiveProperty<string>(_randomWordProperty));
        }

        private void GivenAWordsGatewayThatReturnsAnError()
        {
            GivenAWordsGateway();

            _errorProperty = new ReactiveProperty<string>();

            _wordsGateway.Error.Returns(new ReadOnlyReactiveProperty<string>(_errorProperty));
        }

        private void GivenAStartGameUseCase()
        {
            _startGameUseCase = new StubStartGameUseCase(_hangmanGame, _wordsGateway);
        }

        private void WhenInvoking()
        {
            _startGameUseCase.Execute();
        }

        private void WhenInvokingAndSendingARandomWord(string randomWord)
        {
            _startGameUseCase.Execute();

            _randomWordProperty.Value = randomWord;
        }

        private void WhenInvokingAndSendingAnError(string error)
        {
            _startGameUseCase.Execute();

            _errorProperty.Value = error;
        }

        private void ThenHangmanModelIsReseted()
        {
            _hangmanGame.Received(1).Reset();
        }

        private void ThenGetRandomWordIsCalled()
        {
            _wordsGateway.Received(1).GetRandomWord();
        }

        private void ThenARandomWordWasReceived()
        {
            Assert.IsTrue(_startGameUseCase.RandomWordWasReceived);
        }

        private void ThenAnErrorWasReceived()
        {
            Assert.IsTrue(_startGameUseCase.ErrorWasReceived);
        }

        private class StubStartGameUseCase : StartGameUseCase
        {
            public bool RandomWordWasReceived { get; private set; }
            public bool ErrorWasReceived { get; private set; }

            public StubStartGameUseCase(IHangmanGame hangman, IWordsGateway wordsGateway) : base(hangman, wordsGateway)
            {
            }

            protected override void OnReceivingARandomWord(string randomWord)
            {
                RandomWordWasReceived = true;
            }

            protected override void OnReceivingAnError(string error)
            {
                ErrorWasReceived = true;
            }
        }
    }
}
