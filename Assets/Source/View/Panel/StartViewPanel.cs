using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartViewPanel : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _qute;
    [SerializeField] private TextMeshProUGUI _score;

    public void Show(Action start,Action qute,int score)
    {
        _start.onClick.RemoveAllListeners();
        _qute.onClick.RemoveAllListeners();

        _start.onClick.AddListener(() => start());
        _qute.onClick.AddListener(() => qute());

        _score.text = score.ToString();
    }
}
