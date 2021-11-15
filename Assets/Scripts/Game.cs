using UnityEngine;

class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BalloonSpawner _spawner;
    [SerializeField] private ScorePresenter _scorePresenter;
    [SerializeField] private Sound _sound;

    private ScoreKeeper _score;

    private void Start()
    {
        _score = new ScoreKeeper();
        _scorePresenter.Initialize(_score);

        _spawner.Spawned += OnBalloonSpawned;
        _spawner.StartSpawning();

        Vibration.Init();
    }
    private void OnDestroy()
    {
        _spawner.Spawned -= OnBalloonSpawned;
    }

    private void OnBalloonSpawned(Balloon balloon)
    {
        balloon.Blowed += OnBalloonBlowed;
    }
    private void OnBalloonBlowed(Balloon balloon)
    {
        balloon.Blowed -= OnBalloonBlowed;

        _sound.PlayBlow();
        Vibration.VibratePop();

        _score.Increment();
    }
}