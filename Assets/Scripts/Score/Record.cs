[System.Serializable]
struct Record
{
    public string Name;
    public int Score;

    public Record(string name, int score)
    {
        Name = name;
        Score = score;
    }
}