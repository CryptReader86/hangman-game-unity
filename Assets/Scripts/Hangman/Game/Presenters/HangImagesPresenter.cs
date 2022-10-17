using UniRx;

namespace Hangman.Game.Presenters
{
    public class HangImagesPresenter
    {
        private ReactiveProperty<int> _numErrors;

        public IReadOnlyReactiveProperty<int> NumErrors { get; }

        public HangImagesPresenter()
        {
            _numErrors = new ReactiveProperty<int>();
            NumErrors = new ReadOnlyReactiveProperty<int>(_numErrors);
        }
    }
}
