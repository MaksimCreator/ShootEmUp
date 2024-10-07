using ShootEmUp.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEntryPoint : MonoBehaviour
{
    [SerializeField] private BulletConfig _characterBulletConfig;
    [SerializeField] private BulletConfig _enemyBulletConfig;
    [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
    [SerializeField] private EnemyMovementConfig _enemyMovementConfig;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private StartViewPanel _startViewPanel;

    private void Awake()
    {
        InitializedSingelServiceLocator.TryInitialized(_characterConfig,_characterBulletConfig, _enemyBulletConfig, _enemySpawnerConfig,_enemyMovementConfig);

        SingelPlayerData data = SingelServiceLocator.GetService<SingelPlayerData>();
        _startViewPanel.Show(() => SceneManager.LoadScene(Constant.GamplayScena),() => Application.Quit(),data.AccamulatedScore);
    }
}