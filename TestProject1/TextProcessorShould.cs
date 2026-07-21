using AwesomeAssertions;
using TextProcessing;

namespace TestProject1;
public class TextProcessorShould
{
    [Test]
    public void Analyse_SingleWord_ReturnsWordAsMostUsedAndTotalOfOne()
    {
        var processor = new TextProcessor();
        var result = processor.Analyse("Hello");

        result.Should().Be(
            "Those are the top 10 words used:" +
            "\n1. hello\n" +
            "\nThe text has in total 1 words");
    }
    
    [Test]
    public void Analyse_DifferentSingleWord_ReturnsWordAsMostUsedAndTotalOfOne()
    {
        var processor = new TextProcessor();
        var result = processor.Analyse("Goodbye");

        result.Should().Be(
            "Those are the top 10 words used:" +
            "\n1. goodbye\n" +
            "\nThe text has in total 1 words");
    }
    
    [Test]
    public void Analyse_twoWords_ReturnsWordsAsMostUsedAndTotalOfTwo()
    {
        var processor = new TextProcessor();
        var result = processor.Analyse("You should");

        result.Should().Be(
            "Those are the top 10 words used:" +
            "\n1. you\n" +
            "\n2. should\n" +
            "\nThe text has in total 2 words");
    }
}
