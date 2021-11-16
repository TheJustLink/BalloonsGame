using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sound _sound;
    [SerializeField] private BalloonSpawner _spawner;
    [SerializeField] private ScorePresenter _scorePresenter;
    [SerializeField] private InputWindow _inputWindow;
    [SerializeField] private LeaderboardWindow _leaderboardWindow;

    private List<Balloon> _balloons;
    private ScoreKeeper _score;
    private Saves _saves;

    private void Start()
    {
        _balloons = new List<Balloon>();

        _score = new ScoreKeeper();
        _scorePresenter.Initialize(_score);
        _scorePresenter.Show();

        _saves = new Saves();

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
        _scorePresenter.Hide();

        BlowBalloons();

        _inputWindow.Show(OnInputName);
    }
    private void OnInputName(string name)
    {
        _inputWindow.Hide();

        var record = new Record(name, _score.Value);
        FinalGame(record);
    }

    private void FinalGame(Record record)
    {
        var save = _saves.Load();

        save.Records.AddRecord(record);
        _saves.Save(save);

        _leaderboardWindow.Show(save.Records);

        Invoke(nameof(Restart), 3f);
    }
    private void Restart()
    {
        var currentSceneIndex = gameObject.scene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void BlowBalloons()
    {
        foreach (var balloon in _balloons)
            balloon.Blow();
    }
}