using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{

    public class CharacterInputPresenter
    {
        private ReactiveProperty<string> _wordInProgress;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }

        public CharacterInputPresenter(StartGameUseCase startGameUseCase)
        {
            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            startGameUseCase.WordInProgress.Subscribe(error => _wordInProgress.Value = error);
        }

        public void OnCharacerEntered(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                UnityEngine.Debug.Log(character);
            }
        }
    }    
}
