using NUnit.Framework;
using PokemonWebApi.Controllers;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;

namespace PokemonWebApi.Tests
{
    public class PokemonControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenMewtwo_returns_name()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = sut.Get("mewtwo");

            // Assert
            var name = "mewtwo";
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public void WhenMewtwo_returns_description()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = sut.Get("mewtwo");

            // Assert
            var description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.";
            Assert.AreEqual(description, result.Description);
        }

        [Test]
        public void WhenMewtwo_returns_habitat()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = sut.Get("mewtwo");

            // Assert
            var habitat = "rare";
            var isLegendary = true;
            Assert.AreEqual(habitat, result.Habitat);
        }

        [Test]
        public void WhenMewtwo_returns_isLengendary()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = sut.Get("mewtwo");

            // Assert
            var isLegendary = true;
            Assert.AreEqual(isLegendary, result.IsLegendary);
        }


        [Test]
        public void WhenUnknown_returns_404()
        {
            // Arrange
            var loggerMock = new Moq.Mock<ILogger<PokemonController>>();
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();

            var sut = new PokemonController(loggerMock.Object, pokeApiClientMock.Object);

            // Act
            var result = sut.Get("mewtwo");

            // Assert
            Assert.Fail();
        }
    }
}