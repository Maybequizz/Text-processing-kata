namespace TextProcessing;

public class TextProcessor
{
    public string Analyse(string text)
    {
        var words = text.ToLower().Split(' ');
        var topWords = words
            .Select((w, i) => $"{i + 1}. {w}")
            .Take(10);

        return $"Those are the top 10 words used:\n1. hello\nThe text has in total {words.Length} words";
    }
}
