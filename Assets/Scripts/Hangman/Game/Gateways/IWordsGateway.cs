using UniRx;

namespace Hangman.Game.Gateways
{
    public interface IWordsGateway
    {
        IReadOnlyReactiveProperty<string> RandomWord { get; }
        IReadOnlyReactiveProperty<string> Error { get; }

        void GetRandomWord();
    }
}
