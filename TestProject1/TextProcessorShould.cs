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
            "\n1. hello" +
            "\nThe text has in total 1 words");
    }
}
