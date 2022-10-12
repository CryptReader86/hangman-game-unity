using Hangman.Game.Entities;

namespace Hangman.Game.UseCases
{
    public class AddCharacterUseCase
    {
        private IHangmanGame _hangmanGame;

        public AddCharacterUseCase(IHangmanGame hangmanGame)
        {
            _hangmanGame = hangmanGame;
        }

        public void Execute(char characterToAdd)
        {
            _hangmanGame.AddCharacter(characterToAdd);
        }
    }
}
