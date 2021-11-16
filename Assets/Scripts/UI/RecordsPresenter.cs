using System.Collections.Generic;

using UnityEngine;

class RecordsPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RecordPresenter _recordPresenterPrefab;

    public void Initialize(RecordsKeeper records)
    {
        AddRecords(records.SortedRecords);
    }

    private void AddRecords(IEnumerable<Record> records)
    {
        foreach (var record in records)
            AddRecord(record);
    }
    private void AddRecord(Record record)
    {
        var presenter = Instantiate(_recordPresenterPrefab, transform);
        presenter.Initialize(record);
    }
}