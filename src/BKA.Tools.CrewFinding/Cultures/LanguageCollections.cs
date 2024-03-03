using System.Collections;

namespace BKA.Tools.CrewFinding.Cultures;

public class LanguageCollections : IEnumerable<Language>
{
    private List<Language> Languages { get; }

    private LanguageCollections(IEnumerable<Language> languages)
    {
        Languages = new List<Language>(languages);
    }

    public static LanguageCollections CreateFromAbbrevs(params string[] languagesAbbrevs)
    {
        if(languagesAbbrevs.Length <= 0)
            return Default();
        
        var languages = languagesAbbrevs.Select(abbrevs => new Language(abbrevs));
        return new LanguageCollections(languages);
    }

    public static LanguageCollections Default()
    {
        return new LanguageCollections(new List<Language> {new("EN")});
    }

    public IEnumerator<Language> GetEnumerator()
    {
        return Languages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}