namespace TextProcessing;

public class TextProcessor
{
    public string Analyse(string text)
    {
        var words = text.ToLower().Split(" ");
        var wordslist = words.Select((word, index) => $"\n{index+1}. {word}\n");

        var header = "Those are the top 10 words used:";
        return $"{header}{string.Join("", wordslist)}\nThe text has in total {words.Length} words";
    }
}
