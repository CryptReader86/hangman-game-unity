using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{

    public class CharacterInputPresenter
    {
        private StartGameUseCase _startGameUseCase;
        private AddCharacterUseCase _addCharacterUseCase;

        private ReactiveProperty<bool> _shouldShowCharacterInput;

        public IReadOnlyReactiveProperty<bool> ShouldShowCharacterInput { get; }


        public CharacterInputPresenter(StartGameUseCase startGameUseCase, AddCharacterUseCase addCharacterUseCase)
        {
            _startGameUseCase = startGameUseCase;
            _addCharacterUseCase = addCharacterUseCase;

            _shouldShowCharacterInput = new ReactiveProperty<bool>();
            ShouldShowCharacterInput = new ReadOnlyReactiveProperty<bool>(_shouldShowCharacterInput);

            _startGameUseCase.WordInProgress.Subscribe(_ => SetInputVisibility());
            _addCharacterUseCase.HasWordBeenGuessed.Subscribe(_ => SetInputVisibility());
        }

        public void OnCharacerEntered(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                _addCharacterUseCase.Execute(character[0]);
            }
        }

        private void SetInputVisibility()
        {
            bool isWordInProgressEmpty = string.IsNullOrEmpty(_startGameUseCase.WordInProgress.Value);
            bool hasWordBeenGuessed = _addCharacterUseCase.HasWordBeenGuessed.Value;

            _shouldShowCharacterInput.Value = !(isWordInProgressEmpty || hasWordBeenGuessed);
        }
    }    
}
