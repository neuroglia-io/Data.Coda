using System.IO;
using Xunit;

namespace Neuroglia.Data.Coda.UnitTests.Cases
{

    public class CodaParserTests
    {

        [Fact]
        public void Parse()
        {
            //Arrange
            var parser = new CodaParser();
            var coda = File.ReadAllText("test.coda");

            //Act
            CodaDocument document = parser.Parse(coda);

            //Assert
            Assert.NotNull(document);
            Assert.Equal(coda.Split("\n", System.StringSplitOptions.RemoveEmptyEntries).Length, document.Lines.Count);
        }

    }

}
