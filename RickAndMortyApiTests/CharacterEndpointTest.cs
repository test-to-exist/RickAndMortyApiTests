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


namespace RickAndMortyApiTests
{
    public class CharacterEndpointTest
    {
        [Fact]
        public async void CharacterEndpointShouldReturnSuccessForProperRequest_Test()
        {

            var client = new RestClient("https://rickandmortyapi.com/api/character/1");
            var request = new RestRequest();
            request.AddUrlSegment("id", "1");

            var response = await client.GetAsync(request);
            var character = await client.GetAsync<Character>(request);

            var statusCode = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, statusCode);

            var message = character.name;
            //message.Should().Contain((c) => c.)
            Assert.Equal("Rick Sanchez", message);

        }
    }
}
