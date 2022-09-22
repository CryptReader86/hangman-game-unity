using UnityEngine;
using Zenject;
using Hangman.Game.Presenters;

namespace Hangman.Game.Views
{
    public class StartGameButtonView : MonoBehaviour
    {
        [Inject]
        private StartGameButtonPresenter _presenter;

        public void OnButtonPressed()
        {
            _presenter.StartGame();
        }
    }
}
