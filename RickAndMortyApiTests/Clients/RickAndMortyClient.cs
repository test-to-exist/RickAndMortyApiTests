using System.Threading.Tasks;
using RestSharp;
using RickAndMortyApiTests.DTO;

namespace RickAndMortyApiTests.Clients
{
    public class RickAndMortyClient
    {
        readonly RestClient _client;

        public RickAndMortyClient()
        {
            string _baseUrl = "https://rickandmortyapi.com";
            RestClientOptions _options = new RestClientOptions(_baseUrl);
            _client = new RestClient(_options);
        }

        public Task<RestResponse<Character>> GetCharacter(int id)
        {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", "character");
            request.AddUrlSegment("id", id.ToString());
            return _client.ExecuteAsync<Character>(request);
        }
    }
}
