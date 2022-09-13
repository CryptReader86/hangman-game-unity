using Hangman.Game.Entities;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class StartGameUseCase
    {
        private IHangman _hangman;
        private IWordsGateway _wordsGateway;

        public StartGameUseCase(IHangman hangman, IWordsGateway wordsGateway)
        {
            _hangman = hangman;
            _wordsGateway = wordsGateway;
        }

        public void Execute()
        {
            _hangman.Reset();

            _wordsGateway.GetRandomWord();
        }
    }
}
