using NUnit.Framework;
using Hangman.Game.Entities;
using NSubstitute;
using Hangman.Game.UseCases;

namespace Hangman.Tests.UseCases
{

    public class AddCharacterUseCaseShould
    {
        [TestCase('a')]
        public void Add_A_Character_In_The_Hangman_Game(char characterToAdd)
        {
            IHangmanGame hangmanGame = GivenAHangmanGame();
            AddCharacterUseCase addCharacterUseCase = GivenAnAddCharacterUseCase(hangmanGame);

            WhenExecuting(addCharacterUseCase, characterToAdd);

            ThenACharacterWasAddedInTheHangmanGame(hangmanGame, characterToAdd);
        }

        private IHangmanGame GivenAHangmanGame()
        {
            return Substitute.For<IHangmanGame>();
        }

        private AddCharacterUseCase GivenAnAddCharacterUseCase(IHangmanGame hangmanGame)
        {
            return new AddCharacterUseCase(hangmanGame);
        }

        private void WhenExecuting(AddCharacterUseCase addCharacterUseCase, char characterToAdd)
        {
            addCharacterUseCase.Execute(characterToAdd);
        }

        private void ThenACharacterWasAddedInTheHangmanGame(IHangmanGame hangmanGame, char characterToAdd)
        {
            hangmanGame.Received(1).AddCharacter(characterToAdd);
        }
    }
}
