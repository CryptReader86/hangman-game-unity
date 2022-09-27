using UniRx;

namespace Hangman.Game.UseCases
{
    public class AddCharacterUseCase
    {
        private ReactiveProperty<string> _error;

        public IReadOnlyReactiveProperty<string> Error { get; }

        public void Execute(char characterToAdd)
        {
        }
    }
}
