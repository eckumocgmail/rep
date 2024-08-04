public static class StringExtensions
{
    public static bool ContainsAnyWord2(this string text, string query)
    {
        if (query == null || string.IsNullOrWhiteSpace(query))
            return true;
        foreach (var word in text.ToLower().SplitWords().Where(w => string.IsNullOrWhiteSpace(w) == false))
        {
            foreach (var q in query.ToLower().SplitWords())
            {
                if (word == q)
                    return true;
            }
        }
        return false;


    }
}