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
        private StubStartGameUseCase _startGameUseCase;
        private ReactiveProperty<string> _randomWordProperty;
        private ReactiveProperty<string> _errorProperty;

        private const string TestRandomWord = "blablabla";
        private const string TestError = "Test Error";

        [Test]
        public void Reset_The_Hangman_Model()
        {
            GivenAHangmanModel();
            GivenAWordsGateway();
            GivenAStartGameUseCase();

            WhenInvoking();

            ThenHangmanModelIsReseted();
        }

        [Test]
        public void Try_To_Get_A_Random_Word()
        {
            GivenAHangmanModel();
            GivenAWordsGateway();
            GivenAStartGameUseCase();

            WhenInvoking();

            ThenGetRandomWordIsCalled();
        }

        [Test]
        public void Get_A_Random_Word_When_There_Is_A_Success_Response()
        {
            GivenAHangmanModel();
            GivenAWordsGatewayThatReturnsARandomWord();
            GivenAStartGameUseCase();

            WhenInvokingAndSendingARandomWord(TestRandomWord);

            ThenARandomWordWasReceived();
        }

        [Test]
        public void Get_An_Error_When_There_Is_An_Error_Report()
        {
            GivenAHangmanModel();
            GivenAWordsGatewayThatReturnsAnError();
            GivenAStartGameUseCase();

            WhenInvokingAndSendingAnError(TestError);

            ThenAnErrorWasReceived();
        }

        private void GivenAHangmanModel()
        {
            _hangman = Substitute.For<IHangman>();
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
            _startGameUseCase = new StubStartGameUseCase(_hangman, _wordsGateway);
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
            _hangman.Received(1).Reset();
        }

        private void ThenGetRandomWordIsCalled()
        {
            _wordsGateway.Received(1).GetRandomWord();
        }

        private void ThenARandomWordWasReceived()
        {
            Assert.IsTrue(_startGameUseCase.WasRandomWordReceived());
        }

        private void ThenAnErrorWasReceived()
        {
            Assert.IsTrue(_startGameUseCase.WasAnErrorReceived());
        }

        private class StubStartGameUseCase : StartGameUseCase
        {
            private bool _randomWordWasReceived;
            private bool _errorWasReceived;

            public StubStartGameUseCase(IHangman hangman, IWordsGateway wordsGateway) : base(hangman, wordsGateway)
            {
            }

            public bool WasRandomWordReceived()
            {
                return _randomWordWasReceived;
            }

            public bool WasAnErrorReceived()
            {
                return _errorWasReceived;
            }

            protected override void OnReceivingARandomWord(string randomWord)
            {
                _errorWasReceived = true;
            }

            protected override void OnReceivingAnError(string error)
            {
                _errorWasReceived = true;
            }
        }
    }
}
