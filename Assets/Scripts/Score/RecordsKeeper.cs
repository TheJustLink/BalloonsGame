using System.Collections;
using System.Collections.Generic;

[System.Serializable]
class RecordsKeeper : IEnumerable<Record>
{
    [UnityEngine.SerializeField]
    private List<Record> _records;

    public RecordsKeeper()
    {
        _records = new List<Record>();
    }

    public IReadOnlyList<Record> Records => _records;

    public IEnumerator<Record> GetEnumerator()
    {
        return _records.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _records.GetEnumerator();
    }

    public void AddRecord(Record record)
    {
        _records.Add(record);
    }
}