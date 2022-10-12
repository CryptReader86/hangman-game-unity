using System;

namespace Hangman.Game.Entities
{
    public interface IHangmanGame
    {
        string WordInProgress { get; }

        void Reset();

        void SetWordToGuess(string randomWord);

        void AddCharacter(char characterToAdd);
    }
}
