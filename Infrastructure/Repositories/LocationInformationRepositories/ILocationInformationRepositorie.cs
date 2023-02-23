using Domen.Entities;

namespace Infrastructure.Repositories.LocationInformationRepositories;

public interface ILocationInformationRepositorie
{
    ValueTask<LocationInformation> InsertLocationInformationAsync(LocationInformation locationInformation);
    ValueTask<LocationInformation> SelectLocationInformationAsync(string locationName);
    ValueTask<LocationInformation> UpdateLocationInformationAsync(LocationInformation locationInformation);
    ValueTask<LocationInformation> DelateLocationInformationAsync(LocationInformation locationInformation);
    ValueTask<int> SaveChangesAsync();
}
