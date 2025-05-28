public class IntRange
{
    public int Start { get; set; }
    public int End { get; set; }

    public bool Contains(int value)
    {
        return value >= Start && value <= End;
    }
}
