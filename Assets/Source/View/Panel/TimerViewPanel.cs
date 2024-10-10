using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShootEmUp.View
{
    public class TimerViewPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private int _time;

        private Action _onEnd;

        public void Show(Action onEnd)
        {
            _timeText.text = _time.ToString();

            _onEnd = onEnd;
            gameObject.SetActive(true);
            StartCoroutine(EnableTimer());
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            _onEnd.Invoke();
        }

        private IEnumerator EnableTimer()
        {
            for (int i = _time; i >= 0; i--)
            {
                _timeText.text = i.ToString();
                
                if(i != 0)
                    yield return new WaitForSeconds(1);
            }

            Hide();
        }
    }
}
