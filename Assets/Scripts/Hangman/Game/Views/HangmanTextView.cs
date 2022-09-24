using UnityEngine;
using Zenject;
using Hangman.Game.Presenters;
using TMPro;
using UniRx;

namespace Hangman.Game.Views
{
    public class HangmanTextView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _hangmanText;

        [Inject]
        private HangmanTextPresenter _presenter;

        private void Start()
        {
            _presenter.WordInProgress.Subscribe(SetWordInProgress);
        }

        private void SetWordInProgress(string wordInProgress)
        {
            _hangmanText.text = wordInProgress;
        }
    }
}
