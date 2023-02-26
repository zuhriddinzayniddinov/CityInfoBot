using Domen.Entities;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;

namespace Servics.LocationInformationServic;

public class LocationInfoServic : ILocationInfoServic
{
    private readonly ILocationInformationRepositorie locationInformationRepositorie;

    public LocationInfoServic(
        ILocationInformationRepositorie locationInformationRepositorie)
    {
        this.locationInformationRepositorie = locationInformationRepositorie;
    }

    public async Task<LocationInformation> CreateLocationInformationAsync(
        LocationInformation locationInformation)
    {
        var locationInfo = await this.locationInformationRepositorie.
            InsertLocationInformationAsync(locationInformation);

        return locationInformation;
    }

    public async Task<LocationInformation> DeleteLocationInformationAsync(
        LocationInformation locationInformation)
    {
        var locationInfo = await this.locationInformationRepositorie.
            DelateLocationInformationAsync(locationInformation);

        return locationInfo;
    }

    public async Task<LocationInformation> GetLocationInformationAsync(
        string locationName)
    {
        var locationInformation =
            await this.locationInformationRepositorie.
                SelectLocationInformationAsync(locationName);
        // Add Exception
        return locationInformation;
    }
}
