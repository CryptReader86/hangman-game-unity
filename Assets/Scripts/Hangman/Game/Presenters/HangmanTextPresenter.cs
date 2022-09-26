using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{
    public class HangmanTextPresenter
    {
        private StartGameUseCase _startGameUseCase;

        private ReactiveProperty<string> _wordInProgress;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }

        public HangmanTextPresenter(StartGameUseCase startGameUseCase)
        {
            _startGameUseCase = startGameUseCase;

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            _startGameUseCase.WordInProgress.Subscribe(wordInProgress => _wordInProgress.Value = wordInProgress);
        }
    }
}