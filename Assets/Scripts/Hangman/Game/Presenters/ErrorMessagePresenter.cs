using Hangman.Game.UseCases;
using UniRx;

namespace Hangman.Game.Presenters
{
    public class ErrorMessagePresenter
    {
        private StartGameUseCase _startGameUseCase;

        private ReactiveProperty<string> _error;

        public IReadOnlyReactiveProperty<string> Error { get; }

        public ErrorMessagePresenter(StartGameUseCase startGameUseCase)
        {
            _startGameUseCase = startGameUseCase;

            _error = new ReactiveProperty<string>();
            Error = new ReadOnlyReactiveProperty<string>(_error);

            _startGameUseCase.Error.Subscribe(error => _error.Value = error);
        }
    }
}
