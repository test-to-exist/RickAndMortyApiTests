using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;
using RickAndMortyApiTests.DTO;
using RickAndMortyApiTests.Clients;
using System.Collections.Generic;

public class RickAndMortyApiError
{
    public string errorMessage { get; set; }
}


namespace RickAndMortyApiTests
{
    public class CharacterTest
    {
        static RestClient _client = new RestClient(new RestClientOptions("https://rickandmortyapi.com"));
        static RickAndMortyClient _rickAndMortyClient = new RickAndMortyClient();

        [Fact]
        public async void CharacterEndpointShouldReturnSuccessForProperRequest_Test()
        {
            RestResponse<Character> character = await _rickAndMortyClient.GetCharacter(1);

            var statusCode = character.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var message = character.Data.name;
            message.Should().BeEquivalentTo("Rick Sanchez");
        }

        [Fact]
        public async void CharacterEndpointShouldReturnFailForRequestWithWrongParam_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "character");
            request.AddUrlSegment("id", "0");

            RestResponse<Character> error = await _client.ExecuteAsync<Character>(request);

            error.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        //[Theory]
        [Fact]
        public async void CharacterEndpointWithNameFilterShouldReturnListWIthProperNames_Test()
        {
            RestResponse<List<Character>> characters = await _rickAndMortyClient.GetCharacters(new Dictionary<string, string> { { "name", "Rick" } });

        }
    }
}
