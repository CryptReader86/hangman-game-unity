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
            int errorsToLose = 7;

            var hangmanGame = new HangmanGame();

            var wordsGateway = new WordsGateway("https://api.api-ninjas.com/v1/randomword");

            var startGameUseCase = new StartGameUseCase(hangmanGame, wordsGateway);
            var addCharacterUseCase = new AddCharacterUseCase(hangmanGame);

            var startGameButtonPresenter = new StartGameButtonPresenter(startGameUseCase);
            var errorMessagePresenter = new ErrorMessagePresenter(startGameUseCase);
            var hangmanTextPresenter = new HangmanTextPresenter(startGameUseCase, addCharacterUseCase);
            var characterInputPresenter = new CharacterInputPresenter(errorsToLose, startGameUseCase, addCharacterUseCase);
            var hangImagesPresenter = new HangImagesPresenter(startGameUseCase, addCharacterUseCase);

            Container
                .Bind<StartGameButtonPresenter>()
                .FromInstance(startGameButtonPresenter);

            Container
                .Bind<ErrorMessagePresenter>()
                .FromInstance(errorMessagePresenter);

            Container
                .Bind<HangmanTextPresenter>()
                .FromInstance(hangmanTextPresenter);

            Container
                .Bind<CharacterInputPresenter>()
                .FromInstance(characterInputPresenter);

            Container
                .Bind<HangImagesPresenter>()
                .FromInstance(hangImagesPresenter);
        }
    }
}
