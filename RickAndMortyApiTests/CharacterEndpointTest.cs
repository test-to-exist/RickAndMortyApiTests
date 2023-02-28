using System;
using System.Net;
using RestSharp;
using Xunit;
using FluentAssertions;

public class CharacterResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public Character Content { get; set; }

}


public class Character
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public Origin origin { get; set; }
    public Location location { get; set; }
    public string image { get; set; }
    public string[] episode { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}

public class Origin
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Location
{
    public string name { get; set; }
    public string url { get; set; }
}


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

        [Fact]
        public async void CharacterEndpointShouldReturnSuccessForProperRequest_Test()
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "character");
            request.AddUrlSegment("id", "1");

            RestResponse<Character> character = await _client.ExecuteAsync<Character>(request);

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

            var statusCode = error.StatusCode;
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            var message = error.Data.error;
            Assert.Equal("Character not found", message);
        }
    }
}
