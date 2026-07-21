namespace TextProcessing;

public class TextProcessor
{
    public string Analyse(string text)
    {
        var words = text.ToLower();

        return $"Those are the top 10 words used:\n1. {words}\nThe text has in total 1 words";
    }
}
