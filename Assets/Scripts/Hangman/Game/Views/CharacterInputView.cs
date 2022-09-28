using UnityEngine;
using TMPro;
using Zenject;
using Hangman.Game.Presenters;

namespace Hangman.Game.Views
{
    public class CharacterInputView : MonoBehaviour
    {
        [Inject]
        private StartGameButtonPresenter _presenter;

        public TMP_InputField letterInputText;

        public void OnCharacterEntered()
        {
        }
    }
}
