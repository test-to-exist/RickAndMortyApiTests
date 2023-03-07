using System.Configuration;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RickAndMortyApiTests.DTO;

namespace RickAndMortyApiTests.Clients
{
    public class RickAndMortyClient
    {
        readonly RestClient _client;

        public RickAndMortyClient()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, true)
               .Build();
            string _baseUrl = configuration.GetSection("baseUrl").Value;

            RestClientOptions _options = new RestClientOptions(_baseUrl);
            _client = new RestClient(_options);
        }

        public Task<RestResponse<Character>> GetCharacter(int id)
        {
            return GetEntity<Character>(nameof(Character).ToLower(), id);
        }

        public Task<RestResponse<Location>> GetLocation(int id)
        {
            return GetEntity<Location>(nameof(Location).ToLower(), id);
        }

        private Task<RestResponse<T>> GetEntity<T>(string entity, int id) {
            var request = new RestRequest("/api/{entity}/{id}");
            request.AddUrlSegment("entity", entity);
            request.AddUrlSegment("id", id.ToString());
            return _client.ExecuteAsync<T>(request);
        }
    }
}
