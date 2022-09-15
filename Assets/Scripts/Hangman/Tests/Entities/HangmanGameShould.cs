using NUnit.Framework;
using Hangman.Game.Entities;

namespace Hangman.Tests.Entities
{
    public class HangmanGameShould
    {
        private HangmanGame _hangmanGame;

        [SetUp]
        public void Setup()
        {
            _hangmanGame = new HangmanGame();
        }

        [Test]
        public void Should_Set_Word_To_Guess_To_Empty_When_Resseted()
        {
            WhenHangmanGameIsResetted();

            ThenWordToGuessIsResetted();
        }

        [Test]
        public void Should_Set_Word_In_Progress_To_Empty_When_Resseted()
        {
            WhenHangmanGameIsResetted();

            ThenWordInProgressIsResetted();
        }

        [Test]
        public void Should_Set_Errors_To_Zero_When_Resseted()
        {
            WhenHangmanGameIsResetted();

            ThenNumberOfErrorsIsResseted();
        }

        [Test]
        public void Should_Clear_Added_Letters_When_Resseted()
        {
            WhenHangmanGameIsResetted();

            ThenAddedLettersAreResetted();
        }

        private void WhenHangmanGameIsResetted()
        {
            _hangmanGame.Reset();
        }

        private void ThenWordToGuessIsResetted()
        {
            Assert.AreEqual(_hangmanGame.WordToGuess, string.Empty);
        }

        private void ThenWordInProgressIsResetted()
        {
            Assert.AreEqual(_hangmanGame.WordInProgress, string.Empty);
        }

        private void ThenNumberOfErrorsIsResseted()
        {
            Assert.AreEqual(_hangmanGame.Errors, 0);
        }

        private void ThenAddedLettersAreResetted()
        {
            Assert.AreEqual(_hangmanGame.AddedLetters.Count, 0);
        }
    }
}
