namespace ProgrammingPractice.Interfaces
{
    public interface IApplicationSettings
    {
        string this[string key] { get; }
        string[] AllKeys();
        bool HasKey(string key);
    }
}
