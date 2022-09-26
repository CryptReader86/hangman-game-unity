using System;
using System.Collections.Generic;

namespace Hangman.Game.Entities
{
    public class HangmanGame : IHangmanGame
    {
        public string WordToGuess { get; private set; }
        public string WordInProgress { get; private set; }

        public IList<char> AddedLetters { get; } = new List<char>();

        public int Errors { get; private set; }

        public void Reset()
        {
            WordToGuess = string.Empty;
            WordInProgress = string.Empty;
            Errors = 0;
            AddedLetters.Clear();
        }

        public void SetWordToGuess(string randomWord)
        {
            WordToGuess = randomWord.ToLower();
            WordInProgress = new string('□', WordToGuess?.Length ?? 0);
        }
    }
}
