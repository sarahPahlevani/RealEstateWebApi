using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.DAL.DtoContracts;

namespace RealEstateAgency.Implementations.ApiImplementations.Services.Contracts
{
    public interface IDtoService<TDto>
        where TDto : class, IDto
    {
        IEnumerable<TDto> GetAllDtos();

        Task<IEnumerable<TDto>> GetAllDtosAsync(CancellationToken cancellationToken = default);

        TDto GetDto(int id);

        Task<TDto> GetDtoAsync(int id, CancellationToken cancellationToken = default);

        TDto CreateByDto(TDto value);

        Task<TDto> CreateByDtoAsync(TDto value, CancellationToken cancellationToken = default);

        void UpdateByDto(TDto value);

        Task UpdateByDtoAsync(TDto value, CancellationToken cancellationToken = default);

        int DeleteById(int id);

        Task<int> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
