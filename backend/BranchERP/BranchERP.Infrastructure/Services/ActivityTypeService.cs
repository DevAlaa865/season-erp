using AutoMapper;
using BranchERP.Application.DTOs.ActivityType;
using BranchERP.Application.DTOs.Common;
using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BranchERP.Infrastructure.Services
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IReadOnlyList<ActivityTypeDto>>> GetAllAsync()
        {
            var items = await _unitOfWork.Repository<ActivityType>().GetAllAsync();
            var data = _mapper.Map<IReadOnlyList<ActivityTypeDto>>(items);

            return ApiResponse<IReadOnlyList<ActivityTypeDto>>.Ok(data);
        }

        public async Task<ApiResponse<IReadOnlyList<ActivityTypeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null)
        {
            var repo = _unitOfWork.Repository<ActivityType>();

            Expression<Func<ActivityType, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(search))
                filter = a => a.ActivityName.Contains(search);

            var items = await repo.GetAllAsync(filter);

            var paged = items
                .OrderBy(a => a.ActivityName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var data = _mapper.Map<IReadOnlyList<ActivityTypeDto>>(paged);
            return ApiResponse<IReadOnlyList<ActivityTypeDto>>.Ok(data);
        }

        public async Task<ApiResponse<ActivityTypeDto>> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<ActivityType>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<ActivityTypeDto>.Fail("Activity type not found");

            var dto = _mapper.Map<ActivityTypeDto>(entity);
            return ApiResponse<ActivityTypeDto>.Ok(dto);
        }

        public async Task<ApiResponse<ActivityTypeDto>> CreateAsync(ActivityTypeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<ActivityType>();
            var entity = _mapper.Map<ActivityType>(model);

            await repo.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ActivityTypeDto>(entity);
            return ApiResponse<ActivityTypeDto>.Ok(dto, "Activity type created successfully");
        }

        public async Task<ApiResponse<ActivityTypeDto>> UpdateAsync(int id, ActivityTypeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<ActivityType>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<ActivityTypeDto>.Fail("Activity type not found");

            _mapper.Map(model, entity);
            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<ActivityTypeDto>(entity);
            return ApiResponse<ActivityTypeDto>.Ok(dto, "Activity type updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<ActivityType>();
            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<bool>.Fail("Activity type not found");

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<bool>.Ok(true, "Activity type deleted successfully");
        }
    }
}
