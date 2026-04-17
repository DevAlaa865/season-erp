using AutoMapper;
using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.Country;
using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BranchERP.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ============================
        // Get All
        // ============================
        public async Task<ApiResponse<IReadOnlyList<CountryDto>>> GetAllAsync()
        {
            var repo = _unitOfWork.Repository<Country>();
            var entities = await repo.GetAllAsync();

            var data = _mapper.Map<IReadOnlyList<CountryDto>>(entities);
            return ApiResponse<IReadOnlyList<CountryDto>>.Ok(data);
        }

        // ============================
        // Get Paged
        // ============================
        public async Task<ApiResponse<IReadOnlyList<CountryDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null)
        {
            var repo = _unitOfWork.Repository<Country>();
            IQueryable<Country> query = repo.Query();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.CountryName.Contains(search));

            var paged = await query
                .OrderBy(c => c.CountryName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<IReadOnlyList<CountryDto>>(paged);
            return ApiResponse<IReadOnlyList<CountryDto>>.Ok(data);
        }

        // ============================
        // Get By Id
        // ============================
        public async Task<ApiResponse<CountryDto>> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<Country>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<CountryDto>.Fail("Country not found");

            var dto = _mapper.Map<CountryDto>(entity);
            return ApiResponse<CountryDto>.Ok(dto);
        }

        // ============================
        // Create
        // ============================
        public async Task<ApiResponse<CountryDto>> CreateAsync(CountryCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<Country>();
            var entity = _mapper.Map<Country>(model);

            await repo.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<CountryDto>(entity);
            return ApiResponse<CountryDto>.Ok(dto, "Country created successfully");
        }

        // ============================
        // Update
        // ============================
        public async Task<ApiResponse<CountryDto>> UpdateAsync(int id, CountryCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<Country>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<CountryDto>.Fail("Country not found");

            _mapper.Map(model, entity);
            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<CountryDto>(entity);
            return ApiResponse<CountryDto>.Ok(dto, "Country updated successfully");
        }

        // ============================
        // Delete
        // ============================
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<Country>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<bool>.Fail("Country not found");

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<bool>.Ok(true, "Country deleted successfully");
        }
    }
}
