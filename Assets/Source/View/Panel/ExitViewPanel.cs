using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.View
{
    public class ExitViewPanel : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;

        public void Show(Action exit, Action back)
        {
            _exitButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();

            _exitButton.onClick.AddListener(() => 
            {
                exit();
                Hide();
            });
            
            _backButton.onClick.AddListener(() =>
            {
                back();
                Hide();
            });

            gameObject.SetActive(true);
        }

        private void Hide()
        => gameObject.SetActive(false);
    }
}