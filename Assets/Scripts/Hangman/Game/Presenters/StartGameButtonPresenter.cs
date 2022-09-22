using Hangman.Game.UseCases;

namespace Hangman.Game.Presenters
{
    public class StartGameButtonPresenter
    {
        private StartGameUseCase _startGameUseCase;

        public StartGameButtonPresenter(StartGameUseCase startGameUseCase)
        {
            _startGameUseCase = startGameUseCase;
        }

        public void StartGame()
        {
            _startGameUseCase.Execute();
        }
    }
}
