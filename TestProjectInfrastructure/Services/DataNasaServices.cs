using Newtonsoft.Json;
using TestProject.Infrastructure;
using TestProject.Infrastructure.Models;
using TestProject.Services.Models;

namespace TestProject.Services;

public interface IDataNasaServices
{
    Task<IEnumerable<NasaComet>> GetComets();
    IEnumerable<Recclass> GetRecclassesForDB(IEnumerable<NasaComet> list);
}
public class DataNasaServices : IDataNasaServices
{

    private readonly HttpClient _httpClient;
    public DataNasaServices(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public async Task<IEnumerable<NasaComet>> GetComets()
    {
        Uri uri = new Uri("resource/y77d-th95.json", UriKind.Relative);

        using (var response = await _httpClient.GetAsync(uri))
        {
            response.EnsureSuccessStatusCode();
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        return new JsonSerializer().Deserialize<IEnumerable<NasaComet>>(jsonTextReader);
                    }
                }
            }
        }
    }
    public IEnumerable<Recclass> GetRecclassesForDB(IEnumerable<NasaComet> list)
    {
        return list.Select(x => new Recclass() { Name = x.Recclass }).Distinct();
    }
}
