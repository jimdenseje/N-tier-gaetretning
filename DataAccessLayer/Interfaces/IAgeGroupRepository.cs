using Models;

namespace DataAccessLayer.Interfaces
{
    public interface IAgeGroupRepository
    {
        Task<IEnumerable<AgeGroup>> GetAllAsync();
    }
}
