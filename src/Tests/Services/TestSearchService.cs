using Domain.ApiClients;
using Domain.DataContracts;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Services
{
    public class TestSearchService
    {
        const string PokemonName = "MewTwo";

        [Fact]
        public async Task Should_Not_Call_Translate_When_ShouldTranslate_Is_False()
        {
            //Arrange 
            var pokeAPIClientMock = new Mock<IPokemonAPIClient>();
            pokeAPIClientMock.Setup(x => x.Get(It.IsAny<string>()))
                             .Returns(Task.FromResult(It.IsAny<PokemonDetail>()));

            var translateAPIMock = new Mock<ITranslateAPIClient>();
            translateAPIMock.Setup(x => x.Get(It.IsAny<PokemonDetail>()))
                            .Returns(Task.FromResult(It.IsAny<string>()));

            var logger = Mock.Of<ILogger<SearchService>>();


            var paymentService = new SearchService(logger, pokeAPIClientMock.Object, translateAPIMock.Object);

            //Act
            var expected = await paymentService.FindPokemon(PokemonName, shouldTranslate: false);

            //Assert
            translateAPIMock.Verify(x => x.Get(It.IsAny<PokemonDetail>()), Times.Never());
        }

        [Fact]
        public async Task Should_Call_Translate_When_ShouldTranslate_Is_True()
        {
            //Arrange 
            var pokeAPIClientMock = new Mock<IPokemonAPIClient>();
            pokeAPIClientMock.Setup(x => x.Get(It.IsAny<string>()))
                             .Returns(Task.FromResult(new PokemonDetail()));

            var translateAPIMock = new Mock<ITranslateAPIClient>();
            translateAPIMock.Setup(x => x.Get(It.IsAny<PokemonDetail>()))
                            .Returns(Task.FromResult(It.IsAny<string>()));

            var logger = Mock.Of<ILogger<SearchService>>();


            var paymentService = new SearchService(logger, pokeAPIClientMock.Object, translateAPIMock.Object);

            //Act
            var expected = await paymentService.FindPokemon(PokemonName, shouldTranslate: true);

            //Assert
            translateAPIMock.Verify(x => x.Get(It.IsAny<PokemonDetail>()), Times.Once());
        }

        [Fact]
        public async Task Should_Set_Original_Description_When_Translation_Fails()
        {
            const string description = "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket";

            //Arrange 
            var pokeAPIClientMock = new Mock<IPokemonAPIClient>();
            pokeAPIClientMock.Setup(x => x.Get(It.IsAny<string>()))
                             .Returns(Task.FromResult(new PokemonDetail() { Description = description }));

            var translateAPIMock = new Mock<ITranslateAPIClient>();
            translateAPIMock.Setup(x => x.Get(It.IsAny<PokemonDetail>()))
                            .Throws(new Exception());

            var logger = Mock.Of<ILogger<SearchService>>();


            var paymentService = new SearchService(logger, pokeAPIClientMock.Object, translateAPIMock.Object);

            //Act
            var expected = await paymentService.FindPokemon(PokemonName, shouldTranslate: true);

            //Assert
            translateAPIMock.Verify(x => x.Get(It.IsAny<PokemonDetail>()), Times.Once());
            Assert.Equal(expected.Pokemon.Description, description);
        }
    }
}
