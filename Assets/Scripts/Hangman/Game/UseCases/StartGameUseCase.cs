using Hangman.Game.Entities;
using Hangman.Game.Gateways;
using UniRx;

namespace Hangman.Game.UseCases
{
    public class StartGameUseCase
    {
        private IHangmanGame _hangmanGame;
        private IWordsGateway _wordsGateway;

        public StartGameUseCase(IHangmanGame hangmanGame, IWordsGateway wordsGateway)
        {
            _hangmanGame = hangmanGame;
            _wordsGateway = wordsGateway;

            _wordsGateway.RandomWord.Subscribe<string>(OnReceivingARandomWord);
            _wordsGateway.Error.Subscribe<string>(OnReceivingAnError);
        }

        public void Execute()
        {
            _hangmanGame.Reset();

            _wordsGateway.GetRandomWord();
        }

        protected virtual void OnReceivingARandomWord(string randomWord)
        {
        }

        protected virtual void OnReceivingAnError(string error)
        {
        }
    }
}
