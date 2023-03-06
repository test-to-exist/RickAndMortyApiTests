using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;
using RickAndMortyApiTests.DTO;
using RickAndMortyApiTests.Clients;




namespace RickAndMortyApiTests
{
    public class LocationEndpointTest
    {
        static readonly string _baseUrl = "https://rickandmortyapi.com";
        static RestClientOptions _options = new RestClientOptions(_baseUrl);
        static RestClient _client = new RestClient(_options);
        static RickAndMortyClient _rickAndMortyClient = new RickAndMortyClient();

        [Fact]
        public async void LocationEndpointShouldReturnSuccessForProperRequest_Test()
        {
            //RestResponse<Character> character = await _rickAndMortyClient.GetCharacter(1);

            //var statusCode = character.StatusCode;
            //Assert.Equal(HttpStatusCode.OK, statusCode);

            //var message = character.Data.name;
            //Assert.Equal("Rick Sanchez", message);
        }

        [Fact]
        public async void LocationEndpointShouldReturnFailForRequestWithWrongParam_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "location");
            request.AddUrlSegment("id", "0");

            RestResponse<RickAndMortyApiError> error = await _client.ExecuteAsync<RickAndMortyApiError>(request);

            var statusCode = error.StatusCode;
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            var message = error.Data.errorMessage;
            Assert.Equal("Character not found", message);
        }
    }
}
