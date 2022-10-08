using NUnit.Framework;
using Hangman.Game.Entities;
using System;
using System.Linq;

namespace Hangman.Tests.Entities
{
    public class HangmanGameShould
    {
        private HangmanGame _hangmanGame;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Set_Word_To_Guess_To_Empty_When_Resseted()
        {
            GivenAHangmanGame();

            WhenIsResetted();

            ThenWordToGuessIsResetted();
        }

        [Test]
        public void Set_Word_In_Progress_To_Empty_When_Resseted()
        {
            GivenAHangmanGame();

            WhenIsResetted();

            ThenWordInProgressIsResetted();
        }

        [Test]
        public void Set_Errors_To_Zero_When_Resseted()
        {
            GivenAHangmanGame();

            WhenIsResetted();

            ThenNumberOfErrorsIsResseted();
        }

        [Test]
        public void Clear_Added_Charcters_When_Resseted()
        {
            GivenAHangmanGame();

            WhenIsResetted();

            ThenAddedCharactersAreResetted();
        }

        [TestCase("blue")]
        [TestCase("shoe")]
        [TestCase("frame")]
        public void Set_Word_To_Guess_When_A_Random_Word_Is_Added(string randomWord)
        {
            GivenAHangmanGame();

            WhenSettingWordToGuess(randomWord);

            ThenWordToGuessIsSetted(randomWord);
        }

        [TestCase("Blue", "blue")]
        [TestCase("sHOe", "shoe")]
        [TestCase("fRAME", "frame")]
        public void Set_Word_To_Guess_To_Lowercase_When_A_Random_Word_Is_Added(string randomWordWithCaps, string expectedWord)
        {
            GivenAHangmanGame();

            WhenSettingWordToGuess(randomWordWithCaps);

            ThenWordToGuessIsLowercase(expectedWord);
        }

        [TestCase("blue", "□□□□")]
        [TestCase("shoe", "□□□□")]
        [TestCase("frame", "□□□□□")]
        public void Set_Word_In_Progress_To_Initial_State_When_A_Random_Word_Is_Added(string randomWord, string initialWordInProgress)
        {
            GivenAHangmanGame();

            WhenSettingWordToGuess(randomWord);

            ThenInitialWordInProgressIsSetted(initialWordInProgress);
        }

        [TestCase('a')]
        [TestCase('\'')]
        [TestCase('0')]
        public void Add_A_Character(char characterToAdd)
        {
            GivenAHangmanGame();

            WhenAddingACharacter(characterToAdd);

            ThenTheCharacterWasAdded(characterToAdd);
        }

        [TestCase('A', 'a')]
        [TestCase('\'', '\'')]
        [TestCase('0', '0')]
        [TestCase('Z', 'z')]
        [TestCase('c', 'c')]
        public void Convert_To_Lowercase_An_Added_Character(char characterToAdd, char expectedCharacter)
        {
            GivenAHangmanGame();

            WhenAddingACharacter(characterToAdd);

            ThenTheCharacterWasAdded(expectedCharacter);
        }

        [TestCase('a')]
        public void Not_Add_Duplicated_Characters(char characterToAdd)
        {
            GivenAHangmanGameWithAnAddedCharacter(characterToAdd);

            WhenAddingACharacter(characterToAdd);

            ThenTheCharacterWasAddedOnlyOneTime(characterToAdd);
        }

        private void GivenAHangmanGame()
        {
            _hangmanGame = new HangmanGame();
        }

        private void GivenAHangmanGameWithAnAddedCharacter(char characterToAdd)
        {
            GivenAHangmanGame();

            WhenAddingACharacter(characterToAdd);
        }

        private void WhenIsResetted()
        {
            _hangmanGame.Reset();
        }

        private void WhenSettingWordToGuess(string randomWord)
        {
            _hangmanGame.SetWordToGuess(randomWord);
        }

        private void WhenAddingACharacter(char characterToAdd)
        {
            _hangmanGame.TryToAddCharacter(characterToAdd);
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

        private void ThenAddedCharactersAreResetted()
        {
            Assert.AreEqual(_hangmanGame.AddedCharacters.Count, 0);
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

        private void ThenTheCharacterWasAdded(char characterToAdd)
        {
            Assert.IsTrue(_hangmanGame.AddedCharacters.Contains(characterToAdd));
        }

        private void ThenTheCharacterWasAddedOnlyOneTime(char characterToAdd)
        {
            int characterOccurences =
                _hangmanGame.AddedCharacters.Where(character => character == characterToAdd).Select(character => character).Count();

            Assert.AreEqual(1, characterOccurences);
        }
    }
}
