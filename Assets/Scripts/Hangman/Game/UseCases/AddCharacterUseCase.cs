using Hangman.Game.Entities;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class AddCharacterUseCase
    {
        private IHangmanGame _hangmanGame;

        private ReactiveProperty<string> _wordInProgress;
        private ReactiveProperty<bool> _hasWordBeenGuessed;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }
        public IReadOnlyReactiveProperty<bool> HasWordBeenGuessed { get; }

        public AddCharacterUseCase(IHangmanGame hangmanGame)
        {
            _hangmanGame = hangmanGame;

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            _hasWordBeenGuessed = new ReactiveProperty<bool>();
            HasWordBeenGuessed = new ReadOnlyReactiveProperty<bool>(_hasWordBeenGuessed);
        }

        public void Execute(char characterToAdd)
        {
            _hangmanGame.AddCharacter(characterToAdd);

            _wordInProgress.Value = _hangmanGame.WordInProgress;

            _hasWordBeenGuessed.Value = _hangmanGame.HasWordBeenGuessed();
        }
    }
}
