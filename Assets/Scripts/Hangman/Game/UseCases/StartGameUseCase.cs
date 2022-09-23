using Hangman.Game.Entities;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class StartGameUseCase
    {
        private IHangmanGame _hangmanGame;
        private IWordsGateway _wordsGateway;

        private ReactiveProperty<string> _error;

        public IReadOnlyReactiveProperty<string> Error { get; }

        public StartGameUseCase(IHangmanGame hangmanGame, IWordsGateway wordsGateway)
        {
            _hangmanGame = hangmanGame;
            _wordsGateway = wordsGateway;

            _error = new ReactiveProperty<string>();
            Error = new ReadOnlyReactiveProperty<string>(_error);

            _wordsGateway.RandomWord.Subscribe<string>(OnReceivingARandomWord);
            _wordsGateway.Error.Subscribe<string>(OnReceivingAnError);
        }

        public void Execute()
        {
            _hangmanGame.Reset();

            _wordsGateway.GetRandomWord();
        }

        protected virtual void OnReceivingARandomWord(string randomWord)
        {
        }

        protected virtual void OnReceivingAnError(string error)
        {
            _error.Value = error;
        }
    }
}
