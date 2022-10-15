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

        [TestCase("□a□□", 'a')]
        public void Set_The_Word_In_Progress_When_Adding_A_Character(string wordInProgress, char characterToAdd)
        {
            IHangmanGame hangmanGame = GivenAHangmanGameThatReturnsAWordInProgress(wordInProgress);
            AddCharacterUseCase addCharacterUseCase = GivenAnAddCharacterUseCase(hangmanGame);

            WhenExecuting(addCharacterUseCase, characterToAdd);

            ThenWordInProgressWasSetted(addCharacterUseCase, wordInProgress);
        }

        private IHangmanGame GivenAHangmanGame()
        {
            return Substitute.For<IHangmanGame>();
        }

        private IHangmanGame GivenAHangmanGameThatReturnsAWordInProgress(string wordInProgress)
        {
            IHangmanGame hangmanGame = GivenAHangmanGame();
            hangmanGame.WordInProgress.Returns(wordInProgress);

            return hangmanGame;
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

        private void ThenWordInProgressWasSetted(AddCharacterUseCase addCharacterUseCase, string wordInProgress)
        {
            Assert.AreEqual(wordInProgress, addCharacterUseCase.WordInProgress.Value);
        }
    }
}
