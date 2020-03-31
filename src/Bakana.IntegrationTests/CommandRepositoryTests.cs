using System;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace Bakana.IntegrationTests
{
    [TestFixture]
    public class CommandRepositoryTests : RepositoryTestFixtureBase
    {
        private ICommandRepository Sut { get; set; }
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();

            Sut = new CommandRepository(DbConnectionFactory);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var command = TestData.Commands.DotNetRestore;
            command.StepId = "1";

            // Act
            await Sut.CreateOrUpdate(command);

            // Assert
            var all = await Sut.GetAll("1");
            all.Count.Should().Be(1);
        }
        
    }
}