using System;

using UnityEngine;

using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Canvas))]
class InputWindow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField _inputField;

    private Canvas _canvas;
    private Action<string> _resultCallback;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _inputField.onEndEdit.AddListener(OnEndEdit);

        ForceHide();
    }

    public void Show(Action<string> resultCallback)
    {
        _resultCallback = resultCallback;

        Show();
    }
    public void Show()
    {
        _canvas.enabled = true;
        transform.DOScale(1f, 0.2f);
    }
    public void Hide()
    {
        transform.DOScale(0f, 0.2f)
            .onComplete = () => _canvas.enabled = false;
    }
    public void ForceHide()
    {
        transform.localScale = Vector3.zero;
        _canvas.enabled = false;
    }

    private void OnEndEdit(string text)
    {
        _resultCallback?.Invoke(text);
    }
}