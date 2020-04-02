using Bakana.Core;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.UnitTests
{
    [TestFixture]
    public class ShortIdGeneratorTests
    {
        private IShortIdGenerator Sut { get; set; }

        [SetUp]
        public void Setup()
        {
            Sut = new ShortIdGenerator();
        }
        
        [Test]
        public void Should_Generate_String()
        {
            // Act
            var result = Sut.Generate();
            
            // Assert
            result.Should().NotBeEmpty();
        }
    }
}