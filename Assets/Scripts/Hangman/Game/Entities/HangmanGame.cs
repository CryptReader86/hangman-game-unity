using System.Collections.Generic;
using System.Text;

namespace Hangman.Game.Entities
{
    public class HangmanGame : IHangmanGame
    {
        public string WordToGuess { get; private set; }
        public string WordInProgress => _wordInProgress.ToString();

        public IList<char> AddedCharacters { get; } = new List<char>();

        public int Errors { get; private set; }

        private StringBuilder _wordInProgress = new StringBuilder();

        public void Reset()
        {
            _wordInProgress.Clear();

            WordToGuess = string.Empty;
            Errors = 0;
            AddedCharacters.Clear();
        }

        public void SetWordToGuess(string randomWord)
        {
            WordToGuess = randomWord?.ToLowerInvariant();

            _wordInProgress.Append(new string('□', WordToGuess?.Length ?? 0));

            UnityEngine.Debug.Log("Word to guess: " + WordToGuess);
        }

        public void AddCharacter(char characterToAdd)
        {
            char characterToAddLowerCase = char.ToLowerInvariant(characterToAdd);

            if (AddedCharacters.Contains(characterToAddLowerCase))
                return;

            AddedCharacters.Add(characterToAddLowerCase);

            AddCharacterToWordInProgress(characterToAddLowerCase);
        }

        public bool HasWordBeenGuessed()
        {
            return WordInProgress == WordToGuess;
        }

        private void AddCharacterToWordInProgress(char characterToAddLowerCase)
        {
            for (int i = 0; i < WordToGuess?.Length; i++)
            {
                if (WordToGuess[i] == characterToAddLowerCase)
                {
                    _wordInProgress[i] = characterToAddLowerCase;
                }
            }
        }
    }
}
