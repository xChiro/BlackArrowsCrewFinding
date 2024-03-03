using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Cultures.Excepctions;

namespace BKA.Tools.CrewFinding.Tests.Cultures;

public class LanguageTest
{
    [Theory]
    [InlineData("ESP")]
    [InlineData("English")]
    [InlineData("E")]
    public void Try_to_create_a_language_with_an_invalid_length_then_throw_an_exception(string languageCode)
    {
        // Act
        var act = () => new Language(languageCode);
        
        // Assert
        act.Should().Throw<LanguageNameLengthException>();
    }
}