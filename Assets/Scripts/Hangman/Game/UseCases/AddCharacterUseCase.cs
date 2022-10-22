using Hangman.Game.Entities;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class AddCharacterUseCase
    {
        private IHangmanGame _hangmanGame;

        private ReactiveProperty<string> _wordInProgress;
        private ReactiveProperty<bool> _hasWordBeenGuessed;
        private ReactiveProperty<int> _errors;

        public IReadOnlyReactiveProperty<string> WordInProgress { get; }
        public IReadOnlyReactiveProperty<bool> HasWordBeenGuessed { get; }

        public IReadOnlyReactiveProperty<int> Errors { get; }

        public AddCharacterUseCase(IHangmanGame hangmanGame)
        {
            _hangmanGame = hangmanGame;

            _wordInProgress = new ReactiveProperty<string>();
            WordInProgress = new ReadOnlyReactiveProperty<string>(_wordInProgress);

            _hasWordBeenGuessed = new ReactiveProperty<bool>();
            HasWordBeenGuessed = new ReadOnlyReactiveProperty<bool>(_hasWordBeenGuessed);

            _errors = new ReactiveProperty<int>();
            Errors = new ReadOnlyReactiveProperty<int>(_errors);
        }

        public void Execute(char characterToAdd)
        {
            _hangmanGame.AddCharacter(characterToAdd);

            _wordInProgress.Value = _hangmanGame.WordInProgress;

            _hasWordBeenGuessed.Value = _hangmanGame.HasWordBeenGuessed();

            _errors.Value = _hangmanGame.Errors;
        }
    }
}
