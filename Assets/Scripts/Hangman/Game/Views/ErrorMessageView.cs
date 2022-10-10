using UnityEngine;
using TMPro;
using Zenject;
using Hangman.Game.Presenters;
using UniRx;

namespace Hangman.Game.Views
{
    public class ErrorMessageView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _errorText;

        [Inject]
        private ErrorMessagePresenter _presenter;

        private void Start()
        {
            _presenter.Error.Subscribe(SetErrorText);
        }

        private void SetErrorText(string error)
        {
            _errorText.text = error;
        }
    }
}
