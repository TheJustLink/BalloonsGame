using UnityEngine;

using DG.Tweening;

[RequireComponent(typeof(Canvas))]
class LeaderboardWindow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RecordsPresenter _recordsPresenter;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        ForceHide();
    }

    public void Show(RecordsKeeper records)
    {
        _canvas.enabled = true;
        transform.DOScale(1f, 0.2f);

        _recordsPresenter.Initialize(records);
    }
    public void Hide()
    {
        transform.DOScale(0f, 0.2f)
            .onComplete = () => _canvas.enabled = false;
    }
    public void ForceHide()
    {
        _canvas.enabled = false;
    }
}