using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{
    public class ErrorMessagePresenter
    {
        private ReactiveProperty<string> _error;

        public IReadOnlyReactiveProperty<string> Error { get; }

        public ErrorMessagePresenter(StartGameUseCase startGameUseCase)
        {
            _error = new ReactiveProperty<string>();
            Error = new ReadOnlyReactiveProperty<string>(_error);

            startGameUseCase.Error.Subscribe(error => _error.Value = error);
        }
    }
}
