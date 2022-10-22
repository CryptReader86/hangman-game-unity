using UnityEngine;
using TMPro;
using Zenject;
using Hangman.Game.Presenters;
using UniRx;

namespace Hangman.Game.Views
{
    public class CharacterInputView : MonoBehaviour
    {
        [Inject]
        private CharacterInputPresenter _presenter;

        [SerializeField]
        private TMP_InputField _characterInputText;

        private void Start()
        {
            _presenter.ShouldShowCharacterInput.Subscribe(_characterInputText.gameObject.SetActive);
        }

        public void OnCharacterEntered()
        {
            _presenter.OnCharacerEntered(_characterInputText.text);
        }
    }
}
