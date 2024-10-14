using ShootEmUp.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ShootEmUp.EntryPoint
{
    public class StartEntryPoint : MonoBehaviour
    {
        [SerializeField] private StartViewPanel _startViewPanel;

        private GameData _gameData;

        [Inject]
        private void Construct(GameData gameData)
        => _gameData = gameData;

        private void Awake()
        => _startViewPanel.Show(() => SceneManager.LoadScene(Constant.GamplayScena), () => Application.Quit(), _gameData.MaxScore);
    }
}