using UnityEngine;

using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
class ScorePresenter : MonoBehaviour
{
    [Header("Paramters")]
    [SerializeField] private string _format = "{0}";

    private TextMeshProUGUI _textField;
    private ScoreKeeper _score;
    private Tween _punchScaleTween;

    private void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();

        _punchScaleTween = transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 0, 1)
            .SetAutoKill(false)
            .Pause();
    }

    private void OnEnable()
    {
        if (_score != null)
            _score.Changed += OnScoreChanged;
    }
    private void OnDisable()
    {
        if (_score != null)
            _score.Changed -= OnScoreChanged;
    }

    public void Initialize(ScoreKeeper score)
    {
        _score = score;
        OnEnable();
    }

    private void OnScoreChanged(int value)
    {
        _textField.text = string.Format(_format, value);
        _punchScaleTween.Restart();
    }
}