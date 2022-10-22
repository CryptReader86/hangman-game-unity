using System;

namespace Hangman.Game.Entities
{
    public interface IHangmanGame
    {
        int Errors{ get; }

        string WordInProgress { get; }

        void Reset();

        void SetWordToGuess(string randomWord);

        void AddCharacter(char characterToAdd);

        bool HasWordBeenGuessed();
    }
}
