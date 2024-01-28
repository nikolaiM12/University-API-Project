using Microsoft.Extensions.Caching.Memory;
using OrganizationAPI.Domain.Abstractions.Repository;
using OrganizationAPI.Domain.Abstractions.Services;

public class IdCacheService : IIdCacheService
{
    private readonly IMemoryCache cache;

    public IdCacheService(IMemoryCache cache)
    {
        this.cache = cache;
    }

    public async Task<int> GetIdByCountryName(string countryName, ICountryRepository countryRepository)
    {
        var cacheKey = $"CountryId_{countryName}";

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            int dbId = await countryRepository.GetIdByCountryName(countryName);

            if (dbId != 0)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            }

            return dbId;
        });
    }

    public async Task<int> GetIdByIndustryName(string industryName, ISectorRepository sectorRepository)
    {
        var cacheKey = $"IndustryId_{industryName}";

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            int dbId = await sectorRepository.GetIdByIndustryName(industryName);

            if (dbId != 0)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            }

            return dbId;
        });
    }
}
