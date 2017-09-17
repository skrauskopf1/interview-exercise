namespace Interview_Exercise
{
    public interface IPersist
    {
        IPersist SplitProperties(string delimitedString);
        string JoinProperties();
    }
}
