using Models;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAgeGroupService
    {
        Task<IEnumerable<AgeGroup>> GetAllAgeGroupsAsync();
    }
}
