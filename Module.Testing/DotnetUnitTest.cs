using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Module.Core.Models;
using Module.Infrastructure.Data;
using Module.Infrastructure.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Module.Testing
{
    public class DotnetUnitTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IMapper> _mockMapper;

        public DotnetUnitTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddCharacter_AddsCharacterToDatabase()
        {
            using (var context = new DataContext(_options))
            {
                var repository = new CharacterRepository(context, _mockMapper.Object);

                // Arrange
                var characterToAdd = new Character { Name = "Add Test Character" }; // Create a new character without specifying ID

                // Act
                var characters = await repository.AddCharacter(characterToAdd);

                // Assert
                characters.Should().HaveCount(1); // Since we are adding a single character
            }
        }

        [Fact]
        public async Task DeleteCharacter_ExistingId_RemovesCharacter()
        {
            using (var context = new DataContext(_options))
            {
                context.Characters.AddRange(new List<Character>()
                {
                    new Character { Id = 1, Name = "Test Character 1" },
                    new Character { Id = 2, Name = "Test Character 2" }
                });
                await context.SaveChangesAsync();

                var repository = new CharacterRepository(context, _mockMapper.Object);

                // Arrange
                var existingId = 1;

                // Act
                var remainingCharacters = await repository.DeleteCharacter(existingId);

                // Assert
                remainingCharacters.Should().HaveCount(1); // Verify only one character remains
                remainingCharacters.Should().NotContain(c => c.Id == existingId); // Verify deleted character is gone
            }
        }
    }
}
