using UnityEngine;
using TMPro;
using Zenject;
using Hangman.Game.Presenters;

namespace Hangman.Game.Views
{
    public class CharacterInputView : MonoBehaviour
    {
        [Inject]
        private CharacterInputPresenter _presenter;

        public TMP_InputField characterInputText;

        public void OnCharacterEntered()
        {
            _presenter.OnCharacerEntered(characterInputText.text);
        }
    }
}
