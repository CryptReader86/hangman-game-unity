using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{

    public class CharacterInputPresenter
    {
        private AddCharacterUseCase _addCharacterUseCase;

        private ReactiveProperty<string> _wordInProgress;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }

        public CharacterInputPresenter(StartGameUseCase startGameUseCase, AddCharacterUseCase addCharacterUseCase)
        {
            _addCharacterUseCase = addCharacterUseCase;

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            startGameUseCase.WordInProgress.Subscribe(wordInProgress => _wordInProgress.Value = wordInProgress);
        }

        public void OnCharacerEntered(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                _addCharacterUseCase.Execute(character[0]);
            }
        }
    }    
}
