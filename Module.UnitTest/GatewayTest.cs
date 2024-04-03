using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Infrastructure.Data;
using Module.Infrastructure.Repositories;
using FluentAssertions;
using Moq;
using Module.Core.Models;

namespace Module.UnitTest
{
    public class GatewayTest
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CharacterRepository _characterRepository;
        private readonly DataContext _context;

        public GatewayTest()
        {
            _options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            
            _context = new DataContext(_options);

            _mockMapper = new Mock<IMapper>();

            _characterRepository = new CharacterRepository(_context, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllCharacters_ShouldReturnEmptyList_WhenNoCharacterExists()
        {
            //Arrange

            //Act

            var characters = await _characterRepository.GetAllCharacters();

            //Assert

            characters.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCharacters_ShouldReturnListOfCharacters_WhenCharactersExist()
        {
            //Arrange

            var character1 = new Character { Name = "Character 1" };
            var character2 = new Character { Name = "Character 2" };

            await _context.Characters.AddAsync(character1);
            await _context.Characters.AddAsync(character2);
            await _context.SaveChangesAsync();

            //Act

            var characters = await _characterRepository.GetAllCharacters();

            //Assert

            characters.Should().NotBeEmpty();
            characters.Should().HaveCount(2);
        }

    }

}