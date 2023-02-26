using Domen.Entities;

namespace Servics.LocationInformationServic;

public interface ILocationInfoServic
{
    Task<LocationInformation> GetLocationInformationAsync(string locationName);
    Task<LocationInformation> DeleteLocationInformationAsync(LocationInformation locationInformation);
    Task<LocationInformation> CreateLocationInformationAsync(LocationInformation locationInformation);
}
