using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{
    public class HangmanTextPresenter
    {
        private ReactiveProperty<string> _wordInProgress;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }

        public HangmanTextPresenter(StartGameUseCase startGameUseCase, AddCharacterUseCase addCharacterUseCase)
        {
            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            startGameUseCase.WordInProgress.Subscribe(wordInProgress => _wordInProgress.Value = wordInProgress);
            addCharacterUseCase.WordInProgress.Subscribe(wordInProgress => _wordInProgress.Value = wordInProgress);
        }
    }
}
