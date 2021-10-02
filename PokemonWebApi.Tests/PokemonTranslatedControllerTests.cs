using NUnit.Framework;
using PokemonWebApi.Controllers;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Moq;

namespace PokemonWebApi.Tests
{
    public class PokemonTranslatedControllerTests
    {
        private Moq.Mock<ILogger<PokemonTranslatedController>> _loggerMock;
        private PokemonFactory.IPokemonFactory _pokemonFactory;

        [SetUp]
        public void TestSetup()
        {
            _loggerMock = new Moq.Mock<ILogger<PokemonTranslatedController>>();
            OfflinePokeApiClient offlinePokeApiClient = new();
            _pokemonFactory = new PokemonFactory.PokemonFactory(offlinePokeApiClient);
        }

        [Test]
        public async Task WhenMewtwo_returns_name()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            var name = "mewtwo";
            Assert.AreEqual(name, response.Value.Name);
        }

        [Test]
        public async Task WhenMewtwo_returns_description()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            var description = "Created by a scientist after years of horrific gene splicing and DNA engineering experiments, it was.";
            Assert.AreEqual(description, response.Value.Description);
        }

        [Test]
        public async Task WhenMewtwo_returns_habitat()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            var habitat = "rare";
            Assert.AreEqual(habitat, response.Value.Habitat);
        }

        [Test]
        public async Task WhenMewtwo_returns_isLengendary()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            var isLegendary = true;
            Assert.AreEqual(isLegendary, response.Value.IsLegendary);
        }


        [Test]
        public async Task WhenUnknow_returns_404()
        {
            // Arrange
            var pokeApiClientMock = new Moq.Mock<IPokeApiClient>();
            pokeApiClientMock.Setup(m => m.GetPokemonSpeciesAsync(It.IsAny<string>())).Throws(new HttpRequestException(null, null, HttpStatusCode.NotFound));
            var sut = new PokemonTranslatedController(_loggerMock.Object, new PokemonFactory.PokemonFactory(pokeApiClientMock.Object));


            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, (response.Result as StatusCodeResult).StatusCode);
        }
    }
}