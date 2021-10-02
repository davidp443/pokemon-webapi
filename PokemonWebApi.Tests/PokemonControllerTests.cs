using NUnit.Framework;
using PokemonWebApi.Controllers;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;
using System.Threading.Tasks;

namespace PokemonWebApi.Tests
{
    public class PokemonControllerTests
    {
        private readonly OfflinePokeApiClient offlinePokeApiClient = new();

        [Test]
        public async Task WhenMewtwo_returns_name()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();

            var sut = new PokemonController(loggerMock.Object, offlinePokeApiClient);

            // Act
            var result = await sut.GetAsync("mewtwo");

            // Assert
            var name = "mewtwo";
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public async Task WhenMewtwo_returns_description()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();

            var sut = new PokemonController(loggerMock.Object, offlinePokeApiClient);

            // Act
            var result = await sut.GetAsync("mewtwo");

            // Assert
            var description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
            Assert.AreEqual(description, result.Description);
        }

        [Test]
        public async Task WhenMewtwo_returns_habitat()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();

            var sut = new PokemonController(loggerMock.Object, offlinePokeApiClient);

            // Act
            var result = await sut.GetAsync("mewtwo");

            // Assert
            var habitat = "rare";
            Assert.AreEqual(habitat, result.Habitat);
        }

        [Test]
        public async Task WhenMewtwo_returns_isLengendary()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();

            var sut = new PokemonController(loggerMock.Object, offlinePokeApiClient);

            // Act
            var result = await sut.GetAsync("mewtwo");

            // Assert
            var isLegendary = true;
            Assert.AreEqual(isLegendary, result.IsLegendary);
        }


        [Test]
        public async Task WhenUnknow_returns_404()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = await sut.GetAsync("mewtwo");

            // Assert
            Assert.Fail();
        }
    }
}