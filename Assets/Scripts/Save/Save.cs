[System.Serializable]
class Save
{
    public RecordsKeeper Records;

    public Save()
    {
        Records = new RecordsKeeper();
    }
    public Save(RecordsKeeper records)
    {
        Records = records;
    }
}