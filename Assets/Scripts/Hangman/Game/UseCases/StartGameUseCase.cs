using Hangman.Game.Entities;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class StartGameUseCase
    {
        private IHangmanGame _hangmanGame;
        private IWordsGateway _wordsGateway;

        private ReactiveProperty<int> _errors;
        private ReactiveProperty<string> _wordInProgress;
        private ReactiveProperty<string> _error;

        public IReadOnlyReactiveProperty<int> Errors { get; }
        public IReadOnlyReactiveProperty<string> WordInProgress { get; }
        public IReadOnlyReactiveProperty<string> Error { get; }

        public StartGameUseCase(IHangmanGame hangmanGame, IWordsGateway wordsGateway)
        {
            _hangmanGame = hangmanGame;
            _wordsGateway = wordsGateway;

            _errors = new ReactiveProperty<int>();
            Errors = new ReadOnlyReactiveProperty<int>(_errors);

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            _error = new ReactiveProperty<string>();
            Error = new ReadOnlyReactiveProperty<string>(_error);

            _wordsGateway.RandomWord.Subscribe<string>(OnReceivingARandomWord);
            _wordsGateway.Error.Subscribe<string>(OnReceivingAnError);
        }

        public void Execute()
        {
            _hangmanGame.Reset();

            _errors.Value = _hangmanGame.Errors;

            _wordsGateway.GetRandomWord();
        }

        protected virtual void OnReceivingARandomWord(string randomWord)
        {
            _hangmanGame.SetWordToGuess(randomWord);

            _wordInProgress.Value = _hangmanGame.WordInProgress;
        }

        protected virtual void OnReceivingAnError(string error)
        {
            _error.Value = error;
        }
    }
}
