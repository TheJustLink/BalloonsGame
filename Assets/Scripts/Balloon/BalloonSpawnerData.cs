using UnityEngine;

[CreateAssetMenu(menuName = "Game/BallonSpawner")]
class BalloonSpawnerData : ScriptableObject
{
    public Balloon BalloonPrefab => _balloonPrefab;
    public float SpawnRange => _spawnRange;
    public float SpawnRate => _spawnRate;
    public float SpawnRateAcceleration => _spawnRateAcceleration;

    [Header("References")]
    [SerializeField] private Balloon _balloonPrefab;
    [Header("Parameters")]
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnRateAcceleration;
}