namespace TextProcessing;

public class TextProcessor
{
    public string Analyse(string text)
    {
        var words = text.ToLower().Split(' ');
        return $"Those are the top 10 words used:\n\n1. {words[0]}\n\nThe text has in total {words.Length} words";
    }
}
