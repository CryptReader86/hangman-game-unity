using System;
using System.Collections.Generic;

namespace Hangman.Game.Entities
{
    public class HangmanGame : IHangmanGame
    {
        public string WordToGuess { get; private set; }
        public string WordInProgress { get; private set; }

        public IList<char> AddedCharacters { get; } = new List<char>();

        public int Errors { get; private set; }

        public void Reset()
        {
            WordToGuess = string.Empty;
            WordInProgress = string.Empty;
            Errors = 0;
            AddedCharacters.Clear();
        }

        public void SetWordToGuess(string randomWord)
        {
            WordToGuess = randomWord?.ToLower();
            WordInProgress = new string('□', WordToGuess?.Length ?? 0);
        }

        public void TryToAddCharacter(char characterToAdd)
        {
            AddedCharacters.Add(characterToAdd);
        }
    }
}
