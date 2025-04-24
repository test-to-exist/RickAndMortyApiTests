using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;
using RickAndMortyApiTests.DTO;
using RickAndMortyApiTests.Clients;
using System.Threading.Tasks;




namespace RickAndMortyApiTests
{
    public class EpisodeTest
    {
        static RestClient _client = new RestClient(new RestClientOptions("https://rickandmortyapi.com"));
        static RickAndMortyClient _rickAndMortyClient = new RickAndMortyClient();

        [Fact]
        public async Task EpisodeEndpointShouldReturnSuccessForProperRequest_Test()
        {
            RestResponse<Episode> episode = await _rickAndMortyClient.GetEpisode(1);

            var statusCode = episode.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var message = episode.Data.name;
            message.Should().BeEquivalentTo("Pilot");
        }

        [Fact]
        public async Task EpisodeEndpointShouldReturnFailForRequestWithWrongParam_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "episode");
            request.AddUrlSegment("id", "0");

            RestResponse<RickAndMortyApiError> error = await _client.ExecuteAsync<RickAndMortyApiError>(request);
            error.StatusCode.Should().Be(HttpStatusCode.NotFound);

            //var message = error.Content.name;
            //message.Should().BeEquivalentTo("unknown");
        }
    }
}
