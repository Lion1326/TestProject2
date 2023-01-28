using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestProject.Infrastructure;
using TestProject.Infrastructure.Models;
using TestProject.Services;
using TestProjectApp.CometManagement.Models;

namespace TestProject.App.CometManagement;

public interface IWorkService
{
    Task SaveDataFromNasaAsync();
    Task<GetCometsByYearResponse> GetCometsByYearAsync(GetCometsByYearRequest request);
    Task<GetPropertiesPageResponse> GetPropertiesAsync();
    void ClearDB();
}
public class WorkService : IWorkService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDataNasaServices _services;
    private readonly IMemoryCache _memoryCache;
    public WorkService(IUnitOfWork unitOfWork, IDataNasaServices service, IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _services = service ?? throw new ArgumentNullException(nameof(service));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }
    public async Task SaveDataFromNasaAsync()
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var nasaComets = await _services.GetComets();
            var recclasses = _services.GetRecclassesForDB(nasaComets);
            _unitOfWork.recclassRepository.AddParts(recclasses);
            await _unitOfWork.SaveAsync();

            foreach (var item in nasaComets)
            {
                Comet comet = new Comet();
                comet.ID = item.ID;
                comet.Name = item.Name;
                comet.Year = item.Year;
                comet.Nametype = item.Nametype;
                comet.Fall = item.Fall == "Fell";
                if (item.Geolocation != null)
                    comet.GeolocationType = item.Geolocation.Type;
                comet.Mass = item.Mass;
                comet.Reclat = item.Reclat;
                comet.Reclong = item.Reclong;
                comet.RecclassID = _unitOfWork.recclassRepository.GetByName(item.Recclass).ID;
                _unitOfWork.cometRepository.AddPart(comet);
            }
            _unitOfWork.SaveChangesWithIdentityInsert<Comet>();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            _unitOfWork.Dispose();
            throw;
        }
    }

    public async Task<GetCometsByYearResponse> GetCometsByYearAsync(GetCometsByYearRequest request)
    {
        GetCometsByYearResponse result = new GetCometsByYearResponse();
        result.List = await _unitOfWork.cometRepository.Find(x => x.Year.HasValue &&
                                                (!request.RecclassID.HasValue || x.RecclassID == request.RecclassID) &&
                                                x.Year.Value.Year >= request.YearFrom &&
                                                x.Year.Value.Year <= request.YearTo &&
                                                x.Name.IndexOf(request.Pattern) >= 0)
                                   .GroupBy(x => x.Year.Value.Year)
                                   .Select(x => new CometByYear()
                                   {
                                       Year = x.Key,
                                       Count = x.Count(),
                                       Mass = x.Sum(x => x.Mass)
                                   })
                                   .OrderBy(x => x.Year).ThenBy(x => x.Count).ThenBy(x => x.Mass)
                                   .ToListAsync();
        if (result.List.Any())
        {
            result.TotalMass = result.List.Sum(x => x.Mass);
            result.TotalCount = result.List.Sum(x => x.Count);
        }
        return result;
    }
    public async Task<GetPropertiesPageResponse> GetPropertiesAsync()
    {
        GetPropertiesPageResponse result = null;
        if (!_memoryCache.TryGetValue("PageProperties", out result))
        {
            result = new GetPropertiesPageResponse();
            result.Recclasses = await _unitOfWork.recclassRepository.GetList().ToListAsync();
            result.Years = _unitOfWork.cometRepository.GetYears();
            _memoryCache.Set("PageProperties", result,
            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }
        return result;
    }
    public void ClearDB()
    {
        _unitOfWork.cometRepository.ClearTable();
        _unitOfWork.recclassRepository.ClearTable();
        _memoryCache.Remove("PageProperties");
        _unitOfWork.Save();
    }
}
