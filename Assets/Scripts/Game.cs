using System.Collections.Generic;

using UnityEngine;

class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BalloonSpawner _spawner;
    [SerializeField] private ScorePresenter _scorePresenter;
    [SerializeField] private Sound _sound;

    private List<Balloon> _balloons;
    private ScoreKeeper _score;

    private void Start()
    {
        _balloons = new List<Balloon>();

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
        balloon.OutOfScreen += OnBalloonOutOfScreen;
        balloon.Destroying += OnBalloonDestroying;

        _balloons.Add(balloon);
    }
    private void OnBalloonOutOfScreen(Balloon balloon)
    {
        OnLose();
    }
    private void OnBalloonBlowed(Balloon balloon)
    {
        _sound.PlayBlow();
        Vibration.VibratePop();

        _score.Increment();
    }
    private void OnBalloonDestroying(Balloon balloon)
    {
        balloon.Blowed -= OnBalloonBlowed;
        balloon.Destroying -= OnBalloonDestroying;
        balloon.OutOfScreen -= OnBalloonOutOfScreen;

        _balloons.Remove(balloon);
    }

    private void OnLose()
    {
        _spawner.StopSpawning();

        BlowBalloons();
    }

    private void BlowBalloons()
    {
        foreach (var balloon in _balloons)
            balloon.Blow();
    }
}