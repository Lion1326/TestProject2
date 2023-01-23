using Newtonsoft.Json;
using TestProjectAPI.Services.Models;

namespace TestProjectAPI.Services
{
    public interface IDataNasaServices
    {
        Task<List<Comet>> GetComets();
    }
    public class DataNasaServices : IDataNasaServices
    {

        private readonly HttpClient _httpClient;
        public DataNasaServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Comet>> GetComets()
        {
            Uri uri = new Uri("resource/y77d-th95.json", UriKind.Relative);

            using (var response = await _httpClient.GetAsync(uri))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            var comets = new JsonSerializer().Deserialize<List<Comet>>(jsonTextReader);
                            return comets;
                            // do something with the customer
                        }
                    }
                }
            }
            return null;

        }
    }
}
