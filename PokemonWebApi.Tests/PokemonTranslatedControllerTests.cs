using NUnit.Framework;
using PokemonWebApi.Controllers;
using Microsoft.Extensions.Logging;
using PokemonWebApi.PokeApiClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Moq;
using PokemonWebApi.HttpClient;

namespace PokemonWebApi.Tests
{
    public class PokemonTranslatedControllerTests
    {
        private readonly OfflineHttpClient _offlineHttpClient = new();
        private Moq.Mock<ILogger<PokemonTranslatedController>> _loggerMock;
        private PokemonFactory.IPokemonTranslatedFactory _pokemonFactory;

        [SetUp]
        public void TestSetup()
        {
            _loggerMock = new Moq.Mock<ILogger<PokemonTranslatedController>>();
            _pokemonFactory = new PokemonFactory.PokemonTranslatedFactory(
                new PokemonFactory.PokemonFactory(new PokeApiClient.PokeApiClient(_offlineHttpClient)),
                new FunTranslationsClient.FunTranslationsClient(_offlineHttpClient)
            );
        }

        [Test]
        public async Task When_valid_returns_name()
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
        public async Task When_legendary_returns_shakespeare_description()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            var description = "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";
            Assert.AreEqual(description, response.Value.Description);
        }

        [Test]
        public async Task When_habitat_is_cave_returns_shakespeare_description()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("crobat");

            // Assert
            var description = "So si­ lently through the dark on its four wings that it may not be noticed even when nearby,  it flies.";
            Assert.AreEqual(description, response.Value.Description);
        }

        [Test]
        public async Task When_neither_legendary_nor_cave_returns_yoda_description()
        {
            // Arrange
            var sut = new PokemonTranslatedController(_loggerMock.Object, _pokemonFactory);

            // Act
            var response = await sut.GetAsync("blastoise");

            // Assert
            var description = "Created by a scientist after years of horrific gene splicing and dna engineering experiments,  it was.";
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
            var httpClientMock = new Moq.Mock<IHttpClient>();
            httpClientMock.Setup(m => m.GetStringAsync(It.IsAny<string>())).Throws(new HttpRequestException(null, null, HttpStatusCode.NotFound));
            var pokemonFactory = new PokemonFactory.PokemonTranslatedFactory(
                new PokemonFactory.PokemonFactory(new PokeApiClient.PokeApiClient(httpClientMock.Object)),
                new FunTranslationsClient.FunTranslationsClient(_offlineHttpClient)
            );

            var sut = new PokemonTranslatedController(_loggerMock.Object, pokemonFactory);

            // Act
            var response = await sut.GetAsync("mewtwo");

            // Assert
            Assert.AreEqual((int)HttpStatusCode.NotFound, (response.Result as StatusCodeResult).StatusCode);
        }
    }
}