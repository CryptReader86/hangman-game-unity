using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Hangman.Game.Presenters;
using UniRx;

namespace Hangman.Game.Views
{
    public class HangImagesView : MonoBehaviour
    {
        [SerializeField]
        private Image[] _hangImages;

        [Inject]
        private HangImagesPresenter _presenter;

        private void Start()
        {
            _presenter.Errors.Subscribe(SetCurrentHangImage);
        }

        private void SetCurrentHangImage(int numErrors)
        {
            for (int i = 0; i < _hangImages.Length; i++)
            {
                _hangImages[i].gameObject.SetActive(i == numErrors);
            }
        }
    }
}
