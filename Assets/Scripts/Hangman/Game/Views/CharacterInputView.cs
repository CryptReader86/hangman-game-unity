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
            _presenter.WordInProgress.Subscribe(OnWordInProgressChanged);
        }

        public void OnCharacterEntered()
        {
            _presenter.OnCharacerEntered(_characterInputText.text);
        }

        public void OnWordInProgressChanged(string wordInProgress)
        {
            _characterInputText.gameObject.SetActive(!string.IsNullOrEmpty(wordInProgress));
        }
    }
}
