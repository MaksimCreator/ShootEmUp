using UnityEngine;

public class EnemySpawnPoint : IEnemySpawnPoint
{
    private readonly Transform[] _pointSpawn;

    public EnemySpawnPoint(Transform[] pointSpawn)
    {
        _pointSpawn = pointSpawn;
    }

    public int Count => _pointSpawn.Length;

    public Vector3 GetPosition(int index)
    => _pointSpawn[index].position;

    public Quaternion GetRotation(int index)
    => _pointSpawn[index].rotation;
}
