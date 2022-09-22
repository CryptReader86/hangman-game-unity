using Zenject;
using Hangman.Game.Presenters;
using Hangman.Game.UseCases;
using Hangman.Game.Gateways;
using Hangman.Game.Entities;

namespace Hangman.Game.Context
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            var hangmanGame = new HangmanGame();

            var wordsGateway = new WordsGateway("https://api.api-ninjas.com/v1/randomword");

            var startGameUseCase = new StartGameUseCase(hangmanGame, wordsGateway);

            var startGameButtonPresenter = new StartGameButtonPresenter(startGameUseCase);

            Container
                .Bind<StartGameButtonPresenter>()
                .FromInstance(startGameButtonPresenter);
        }
    }
}