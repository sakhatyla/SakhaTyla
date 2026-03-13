namespace SakhaTyla.Core.SpellCheck
{
    public interface ISpellCheckService
    {
        string FixSpelling(string language, string text);
    }
}
