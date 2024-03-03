namespace BKA.Tools.CrewFinding.Cultures.Excepctions;

public class LanguageNameLengthException : Exception 
{
    public LanguageNameLengthException(string message = "Language code must be 2 characters long") : base(message) { }
}