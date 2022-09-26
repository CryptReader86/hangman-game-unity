using NUnit.Framework;
using Hangman.Game.Entities;
using System;

namespace Hangman.Tests.Entities
{
    public class HangmanGameShould
    {
        private HangmanGame _hangmanGame;

        [SetUp]
        public void Setup()
        {
            GivenAHangmanGame();
        }

        [Test]
        public void Set_Word_To_Guess_To_Empty_When_Resseted()
        {
            WhenIsResetted();

            ThenWordToGuessIsResetted();
        }

        [Test]
        public void Set_Word_In_Progress_To_Empty_When_Resseted()
        {
            WhenIsResetted();

            ThenWordInProgressIsResetted();
        }

        [Test]
        public void Set_Errors_To_Zero_When_Resseted()
        {
            WhenIsResetted();

            ThenNumberOfErrorsIsResseted();
        }

        [Test]
        public void Clear_Added_Letters_When_Resseted()
        {
            WhenIsResetted();

            ThenAddedLettersAreResetted();
        }

        [TestCase("blue")]
        [TestCase("shoe")]
        [TestCase("frame")]
        public void Set_Word_To_Guess_When_A_Random_Word_Is_Added(string randomWord)
        {
            WhenSettingWordToGuess(randomWord);

            ThenWordToGuessIsSetted(randomWord);
        }

        [TestCase("Blue", "blue")]
        [TestCase("sHOe", "shoe")]
        [TestCase("fRAME", "frame")]
        public void Set_Word_To_Guess_To_Lowercase_When_A_Random_Word_Is_Added(string randomWordWithCaps, string expectedWord)
        {
            WhenSettingWordToGuess(randomWordWithCaps);

            ThenWordToGuessIsLowercase(expectedWord);
        }

        [TestCase("blue", "□□□□")]
        [TestCase("shoe", "□□□□")]
        [TestCase("frame", "□□□□□")]
        public void Set_Word_In_Progress_To_Initial_State_When_A_Random_Word_Is_Added(string randomWord, string initialWordInProgress)
        {
            WhenSettingWordToGuess(randomWord);

            ThenInitialWordInProgressIsSetted(initialWordInProgress);
        }

        private void GivenAHangmanGame()
        {
            _hangmanGame = new HangmanGame();
        }

        private void WhenIsResetted()
        {
            _hangmanGame.Reset();
        }

        private void WhenSettingWordToGuess(string randomWord)
        {
            _hangmanGame.SetWordToGuess(randomWord);
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

        private void ThenWordToGuessIsSetted(string randomWord)
        {
            Assert.AreEqual(randomWord, _hangmanGame.WordToGuess);
        }

        private void ThenInitialWordInProgressIsSetted(string initialWordInProgress)
        {
            Assert.AreEqual(initialWordInProgress, _hangmanGame.WordInProgress);
        }

        private void ThenWordToGuessIsLowercase(string expectedWord)
        {
            Assert.AreEqual(expectedWord, _hangmanGame.WordToGuess);
        }
    }
}
