using BKA.Tools.CrewFinding.Cultures.Excepctions;

namespace BKA.Tools.CrewFinding.Cultures;

public class Language
{
    public string LanguageCode { get; private set; }

    public Language(string languageCode)
    {
        Validate(languageCode);

        LanguageCode = languageCode;
    }

    private static void Validate(string languageCode)
    {
        if (languageCode.Length != 2)
            throw new LanguageNameLengthException();
    }
}