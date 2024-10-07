using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.View
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private Button _reset;
        [SerializeField] private Button _exit;
        [SerializeField] private TextMeshProUGUI _score;

        public void Show(Action reset,Action exit,int score)
        {
            _score.text = score.ToString();

            _reset.onClick.RemoveAllListeners();
            _exit.onClick.RemoveAllListeners();

            _reset.onClick.AddListener(() => reset());
            _exit.onClick.AddListener(() => exit());

            gameObject.SetActive(true);
        }
    }
}
