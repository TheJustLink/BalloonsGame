using UnityEngine;

using TMPro;

class RecordPresenter : MonoBehaviour
{
    public const string TextFormat = "{0} - {1}";

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _textField;

    public void Initialize(Record record)
    {
        _textField.text = string.Format(TextFormat, record.Name, record.Score);
    }
}