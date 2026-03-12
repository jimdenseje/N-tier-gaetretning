using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Models;

namespace BusinessLogicLayer.Services
{
    public class AgeGroupService : IAgeGroupService
    {
        private readonly IAgeGroupRepository _ageGroupRepository;

        public AgeGroupService(IAgeGroupRepository ageGroupRepository)
        {
            _ageGroupRepository = ageGroupRepository;
        }

        public async Task<IEnumerable<AgeGroup>> GetAllAgeGroupsAsync()
        {
            return await _ageGroupRepository.GetAllAsync();
        }
    }
}
