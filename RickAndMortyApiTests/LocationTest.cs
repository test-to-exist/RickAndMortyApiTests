using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;
using RickAndMortyApiTests.DTO;
using RickAndMortyApiTests.Clients;




namespace RickAndMortyApiTests
{
    public class LocationTest
    {
        static RestClient _client = new RestClient(new RestClientOptions("https://rickandmortyapi.com"));
        static RickAndMortyClient _rickAndMortyClient = new RickAndMortyClient();

        [Fact]
        public async void LocationEndpointShouldReturnSuccessForProperRequest_Test()
        {
            RestResponse<Location> location = await _rickAndMortyClient.GetLocation(1);

            var statusCode = location.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var message = location.Data.name;
            message.Should().BeEquivalentTo("Earth (C-137)");
        }

        [Fact]
        public async void LocationEndpointShouldReturnFailForRequestWithWrongParam_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "location");
            request.AddUrlSegment("id", "0");

            RestResponse<RickAndMortyApiError> error = await _client.ExecuteAsync<RickAndMortyApiError>(request);
            error.StatusCode.Should().Be(HttpStatusCode.NotFound);

            //var message = error.Content.name;
            //message.Should().BeEquivalentTo("unknown");
        }
    }
}
