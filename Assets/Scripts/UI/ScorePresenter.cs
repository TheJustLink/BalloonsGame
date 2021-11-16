using UnityEngine;

using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(Canvas))]
class ScorePresenter : MonoBehaviour
{
    [Header("Paramters")]
    [SerializeField] private string _format = "{0}";

    private Canvas _canvas;
    private TextMeshProUGUI _textField;
    private ScoreKeeper _score;
    private Tween _punchScaleTween;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _textField = GetComponent<TextMeshProUGUI>();

        CreatePunchScaleTween();

        ForceHide();
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
    
    public void Show()
    {
        _canvas.enabled = true;
        transform.DOScale(1f, 0.5f);
    }
    public void Hide()
    {
        transform.DOScale(0f, 0.5f)
            .onComplete = () => _canvas.enabled = false;
    }
    public void ForceHide()
    {
        _canvas.enabled = false;
        transform.localScale = Vector3.zero;
    }

    private void CreatePunchScaleTween()
    {
        _punchScaleTween = transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 0, 1)
            .SetAutoKill(false)
            .Pause();
    }
    private void OnScoreChanged(int value)
    {
        _textField.text = string.Format(_format, value);
        _punchScaleTween.Restart();
    }
}