using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Module.Core.Interfaces;
using Module.Core.Models;
using Module.Infrastructure.Data;
using Module.Infrastructure.Repositories;
using Module.Service.Services;
using Moq;

namespace TestProjectGateway
{
    public class UnitTest1
    {

        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CharacterRepository _repository;
        private readonly DataContext _context;

        public UnitTest1()
        {

            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _mockMapper = new Mock<IMapper>();
            _context = new DataContext(_options);
            
            _repository = new CharacterRepository(_context, _mockMapper.Object);
            using (_context)
            {
                _context.Characters.AddRange(new List<Character>()
                {
                    new Character {Name = "Test Character 1"},
                    new Character {Name = "Test Character 2"}
                });
                _context.SaveChanges();
            }
        }

        [Fact]
        public void DeleteCharacter_ExistingId_RemovesCharacter()
        {
            // Arrange
            var existingId = 1;

            // Act
            var remainingCharacters = _repository.DeleteCharacter(existingId).Result;

            // Assert
            remainingCharacters.Should().HaveCount(1);
            remainingCharacters.Should().NotContain(c => c.Id == existingId);
        }

        [Fact]
        public void AddCharacter_AddsCharacterToDatabase()
        {
            // Arrange

            var characterToAdd = new Character { Name = "Add Test Character" };

            // Act
            var characters = _repository.AddCharacter(characterToAdd).Result;

            // Assert
            characters.Should().HaveCount(3);
        }







        //[Fact]
        //public async Task AddCharacter_ValidCharacter_ReturnsListOfCharacters()
        //{
        //    var options = new DbContextOptionsBuilder<DataContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDB")
        //        .Options;

        //    var mapperMock = new Mock<IMapper>();

        //    using (var context = new DataContext(options))
        //    { 
        //        var repo = new CharacterRepository(context, mapperMock.Object);
        //        var character = new Character { Name = "TestChar" };
        //        var result  = await repo.AddCharacter(character);

        //        Assert.NotNull(result);
        //        Assert.IsType<List<Character>> (result);
        //        Assert.Contains(character, result);
        //    }
        //}

        //[Fact]
        //public async Task GetAllCharacters_ReturnListOfCharacters()
        //{
        //    //Arrange
        //    var characters = new List<Character>()
        //    {
        //        new Character { Id = 1, Name = "Character 1" },
        //        new Character { Id = 2, Name = "Character 2" },
        //        new Character { Id = 3, Name = "Character 3" }
        //    };

        //    var repoMock = new Mock<ICharacterRepository>();

        //    repoMock.Setup(repo => repo.GetAllCharacters()).ReturnsAsync(characters);

        //    //Act

        //    var result = await repoMock.Object.GetAllCharacters();

        //    //Assert

        //    Assert.NotNull(result);
        //    Assert.IsType<List<Character>>(result);
        //    Assert.Equal(characters, result);
        //}

        //[Fact]

        //public async Task DeleteCharacter()
        //{
        //    //Arrange
        //    var characters = new List<Character>()
        //    {
        //        new Character { Id = 1, Name = "Character 1" },
        //        new Character { Id = 2, Name = "Character 2" },
        //        new Character { Id = 3, Name = "Character 3" }
        //    };


        //    var repoMock = new Mock<ICharacterRepository>();

        //    repoMock.Setup(repo => repo.DeleteCharacter(2)).ReturnsAsync(characters);

        //    //Act

        //    var result = await repoMock.Object.DeleteCharacter(2);

        //    Assert.NotNull(result);
        //    Assert.IsType<List<Character>>(result);
        //    Assert.Equal(characters, result);
        //}

        //[Fact]

        //public async Task UpdateCharacter()
        //{

        //    //Arrange

        //    var characterToUpdate = new Character { Id = 1, Name = "TestUpdate" };

        //    var repositoryMock = new Mock<ICharacterRepository>();

        //    repositoryMock.Setup(repo => repo.UpdateCharacter(characterToUpdate)).ReturnsAsync(characterToUpdate);

        //    //Act

        //    var updateCharacter  = await repositoryMock.Object.UpdateCharacter(characterToUpdate);

        //    //Assert

        //    Assert.NotNull(updateCharacter);
        //    Assert.Equal(characterToUpdate.Id, updateCharacter.Id);
        //    Assert.Equal("TestUpdate", updateCharacter.Name);
        //}
    }
}