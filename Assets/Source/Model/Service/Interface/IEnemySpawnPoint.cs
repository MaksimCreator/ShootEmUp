using UnityEngine;

public interface IEnemySpawnPoint
{
    int Count { get; }
    Vector3 GetPosition(int index);
    Quaternion GetRotation(int index);
}