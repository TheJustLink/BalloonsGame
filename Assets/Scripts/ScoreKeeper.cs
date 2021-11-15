using System;

class ScoreKeeper
{
    public int Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value))
                return;

            _value = value;
            Changed?.Invoke(value);
        }
    }
    private int _value;

    public event Action<int> Changed;

    public void Increment() => Value++;
}