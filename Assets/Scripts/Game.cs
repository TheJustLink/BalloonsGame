using UnityEngine;

class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BalloonSpawner _spawner;

    private void Start()
    {
        _spawner.StartSpawning();
    }
}