using AutoMapper;
using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.ShortageType;
using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BranchERP.Infrastructure.Services
{
    public class ShortageTypeService : IShortageTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShortageTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IReadOnlyList<ShortageTypeDto>>> GetAllAsync()
        {
            var repo = _unitOfWork.Repository<ShortageType>();
            var items = await repo.GetAllAsync();

            var data = _mapper.Map<IReadOnlyList<ShortageTypeDto>>(items);
            return ApiResponse<IReadOnlyList<ShortageTypeDto>>.Ok(data);
        }

        public async Task<ApiResponse<IReadOnlyList<ShortageTypeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null)
        {
            var repo = _unitOfWork.Repository<ShortageType>();

            Expression<Func<ShortageType, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(search))
                filter = s => s.ShortageName.Contains(search);

            var items = await repo.GetAllAsync(filter);

            var paged = items
                .OrderBy(s => s.ShortageName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var data = _mapper.Map<IReadOnlyList<ShortageTypeDto>>(paged);
            return ApiResponse<IReadOnlyList<ShortageTypeDto>>.Ok(data);
        }

        public async Task<ApiResponse<ShortageTypeDto>> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<ShortageType>();

            var items = await repo.GetAllAsync(s => s.Id == id);
            var entity = items.FirstOrDefault();

            if (entity is null)
                return ApiResponse<ShortageTypeDto>.Fail("Shortage type not found");

            var dto = _mapper.Map<ShortageTypeDto>(entity);
            return ApiResponse<ShortageTypeDto>.Ok(dto);
        }

        public async Task<ApiResponse<ShortageTypeDto>> CreateAsync(ShortageTypeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<ShortageType>();
            var entity = _mapper.Map<ShortageType>(model);

            await repo.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ShortageTypeDto>(entity);
            return ApiResponse<ShortageTypeDto>.Ok(dto, "Shortage type created successfully");
        }

        public async Task<ApiResponse<ShortageTypeDto>> UpdateAsync(int id, ShortageTypeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<ShortageType>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<ShortageTypeDto>.Fail("Shortage type not found");

            _mapper.Map(model, entity);
            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ShortageTypeDto>(entity);
            return ApiResponse<ShortageTypeDto>.Ok(dto, "Shortage type updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<ShortageType>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<bool>.Fail("Shortage type not found");

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<bool>.Ok(true, "Shortage type deleted successfully");
        }
    }
}
