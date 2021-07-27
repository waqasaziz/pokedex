using Domain.DataContracts;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace Tests.Helpers
{
    public class TestPokemonController
    {
        private const int OkResultCode = 200;
        private const int NotFoundResultCode = 404;
        private const string PokemonName = "mewtwo";

        [Fact]
        public async Task Should_Return_OK_When_Pokemon_Identified()
        {
            // Arrange
            var searchServiceMock = new Mock<ISearchService>();

            searchServiceMock
                .Setup(x => x.FindPokemon(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns((string x, bool y) => Task.FromResult(new SearchResult { Status = SearchResultStatus.Found, Pokemon = new PokemonDetail() }));

            var controller = new PokemonController(searchServiceMock.Object);

            //Act
            var result = (await controller.Get(PokemonName)) as OkObjectResult;

            // Assert
            searchServiceMock.Verify(x => x.FindPokemon(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            Assert.Equal(result.StatusCode, OkResultCode);
            Assert.IsType<PokemonDetail>(result.Value);
        }

        [Fact]
        public async Task Should_Return_NotFound_Request_When_Pokemon__Not_Identified()
        {
            /// Arrange
            var searchServiceMock = new Mock<ISearchService>();

            searchServiceMock
                .Setup(x => x.FindPokemon(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns((string x, bool y) => Task.FromResult(new SearchResult { Status = SearchResultStatus.NotFound }));

            var controller = new PokemonController(searchServiceMock.Object);

            //Act
            var result = (await controller.Get(PokemonName)) as NotFoundResult;

            // Assert
            Assert.Equal(result.StatusCode, NotFoundResultCode);
        }

    }
}
