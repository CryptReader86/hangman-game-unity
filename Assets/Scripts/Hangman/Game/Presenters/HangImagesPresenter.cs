using UniRx;
using Hangman.Game.UseCases;

namespace Hangman.Game.Presenters
{
    public class HangImagesPresenter
    {
        private ReactiveProperty<int> _errors;

        public IReadOnlyReactiveProperty<int> Errors { get; }

        public HangImagesPresenter(StartGameUseCase startGameUseCase)
        {
            _errors = new ReactiveProperty<int>();
            Errors = new ReadOnlyReactiveProperty<int>(_errors);

            startGameUseCase.Errors.Subscribe(errors => _errors.Value = errors);
        }
    }
}
