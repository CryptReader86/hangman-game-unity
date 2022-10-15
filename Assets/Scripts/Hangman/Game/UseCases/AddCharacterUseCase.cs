using Hangman.Game.Entities;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class AddCharacterUseCase
    {
        private IHangmanGame _hangmanGame;

        private ReactiveProperty<string> _wordInProgress;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }

        public AddCharacterUseCase(IHangmanGame hangmanGame)
        {
            _hangmanGame = hangmanGame;

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);
        }

        public void Execute(char characterToAdd)
        {
            _hangmanGame.AddCharacter(characterToAdd);

            _wordInProgress.Value = _hangmanGame.WordInProgress;
        }
    }
}
