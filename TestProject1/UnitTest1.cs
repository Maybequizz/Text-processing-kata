namespace TestProject1;

using TextProcessing;
using AwesomeAssertions;

public class TextProcessorTests
{
    [Test]
    public void Analyse_SingleWord_ReturnsWordAsMostUsedAndTotalOfOne()
    {
        var processor = new TextProcessor();
        var result = processor.Analyse("Hello");

        result.Should().Be(
            "Those are the top 10 words used:" +
            "\n\n1. hello" +
            "\n\nThe text has in total 1 words");
    }
}