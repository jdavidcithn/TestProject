using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Module.Core.Models;
using Module.Infrastructure.Data;
using Module.Infrastructure.Repositories;
using Moq;

namespace TestProjectGateway
{
    public class UnitTest1
    {
        [Fact]
        public async Task AddCharacter_ValidCharacter_ReturnsListOfCharacters()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var mapperMock = new Mock<IMapper>();

            using (var context = new DataContext(options))
            { 
                var repo = new CharacterRepository(context, mapperMock.Object);
                var character = new Character { Name = "TestChar" };
                var result  = await repo.AddCharacter(character);

                Assert.NotNull(result);
                Assert.IsType<List<Character>> (result);
                Assert.Contains(character, result);
            }
        }
    }
}