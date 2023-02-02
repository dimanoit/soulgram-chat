namespace Soulgram.Chat.Services.Validation.Rules;

public static class CustomValidatorRules
{
    public static bool IsGuid(string bar)
    {
        return Guid.TryParse(bar, out _);
    }

    public static bool IsArrayHasDuplicates(ICollection<string> collection)
    {
        return collection.Count == new HashSet<string>(collection).Count;
    }
}