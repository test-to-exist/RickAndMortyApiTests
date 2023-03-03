using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;
using RickAndMortyApiTests.DTO;
using RickAndMortyApiTests.Clients;



public class Error
{
    public string error { get; set; }
}


namespace RickAndMortyApiTests
{
    public class CharacterEndpointTest
    {
        static readonly string _baseUrl = "https://rickandmortyapi.com";
        static RestClientOptions _options = new RestClientOptions(_baseUrl);
        static RestClient _client = new RestClient(_options);
        static RickAndMortyClient _rickAndMortyClient = new RickAndMortyClient();

        [Fact]
        public async void CharacterEndpointShouldReturnSuccessForProperRequest_Test()
        {
            //var request = new RestRequest("/api/{entity}/{id}");
            //request.AddUrlSegment("entity", "character");
            //request.AddUrlSegment("id", "1");

            //RestResponse<Character> character = await _client.ExecuteAsync<Character>(request);
            RestResponse<Character> character = await _rickAndMortyClient.GetCharacter(1);

            var statusCode = character.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var message = character.Data.name;
            Assert.Equal("Rick Sanchez", message);
        }

        [Fact]
        public async void CharacterEndpointShouldReturnFailForRequestWithWrongParam_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "character");
            request.AddUrlSegment("id", "0");

            RestResponse<Error> error = await _client.ExecuteAsync<Error>(request);

            //RestResponse<Error> character = await _client.GetCharacter(0);


            var statusCode = error.StatusCode;
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            var message = error.Data.error;
            Assert.Equal("Character not found", message);
        }
    }
}
