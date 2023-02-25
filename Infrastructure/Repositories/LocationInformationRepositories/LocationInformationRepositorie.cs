using Domen.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LocationInformationRepositories;

public class LocationInformationRepositorie : ILocationInformationRepositorie
{
    private readonly ApplicationDbContext applicationDbContext;

    public LocationInformationRepositorie(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async ValueTask<LocationInformation> DelateLocationInformationAsync(LocationInformation locationInformation)
    {
        var locationInfo = this.applicationDbContext
            .Set<LocationInformation>()
            .Remove(locationInformation);

        await this.SaveChangesAsync();

        return locationInformation;
    }

    public async ValueTask<LocationInformation> InsertLocationInformationAsync(LocationInformation locationInformation)
    {
        var locationInfo = await this.applicationDbContext
            .Set<LocationInformation>()
            .AddAsync(locationInformation);

        await this.SaveChangesAsync();

        return locationInformation;
    }


    public async ValueTask<LocationInformation> SelectLocationInformationAsync(string locationName)
    {
        var locationInformation = await this.applicationDbContext
            .Set<LocationInformation>()
            .FirstOrDefaultAsync(x => x.LocationName == locationName);

        return locationInformation;
    }

    public async ValueTask<LocationInformation> UpdateLocationInformationAsync(LocationInformation locationInformation)
    {
        var locationInfo = this.applicationDbContext
            .Set<LocationInformation>()
            .Update(locationInformation);

        await this.SaveChangesAsync();

        return locationInformation;
    }
    public async ValueTask<int> SaveChangesAsync()
    {
        return await this.applicationDbContext.SaveChangesAsync();
    }
}
