using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BranchERP.Application.DTOs.BranchDailyTarget;
using BranchERP.Application.DTOs.Common;
using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;

namespace BranchERP.Infrastructure.Services
{
    public class BranchDailyTargetService : IBranchDailyTargetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchDailyTargetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BranchDailyTargetHeaderDto>> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<BranchDailyTargetHeader>();

            var entity = await repo.GetAsync(
                x => x.Id == id,
                include: q => q
                    .Include(h => h.Branch)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.Employee)
            );

            if (entity == null)
                return ApiResponse<BranchDailyTargetHeaderDto>.Fail("Target not found");

            var dto = _mapper.Map<BranchDailyTargetHeaderDto>(entity);
            return ApiResponse<BranchDailyTargetHeaderDto>.Ok(dto);
        }

        public async Task<ApiResponse<IReadOnlyList<BranchDailyTargetHeaderDto>>> GetByBranchAndDateAsync(
            int branchId,
            DateTime date)
        {
            var repo = _unitOfWork.Repository<BranchDailyTargetHeader>();

            var items = await repo.GetAllAsync(
                filter: x => x.BranchId == branchId && x.TargetDate.Date == date.Date,
                include: q => q
                    .Include(h => h.Branch)
                    .Include(h => h.Details)
                        .ThenInclude(d => d.Employee)
            );

            var data = _mapper.Map<IReadOnlyList<BranchDailyTargetHeaderDto>>(items);
            return ApiResponse<IReadOnlyList<BranchDailyTargetHeaderDto>>.Ok(data);
        }

        public async Task<ApiResponse<BranchDailyTargetHeaderDto>> CreateAsync(BranchDailyTargetHeaderCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<BranchDailyTargetHeader>();

            var entity = _mapper.Map<BranchDailyTargetHeader>(model);

            entity.Details.Clear();
            foreach (var d in model.Details)
                entity.Details.Add(_mapper.Map<BranchDailyTargetDetail>(d));

            await repo.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<ApiResponse<BranchDailyTargetHeaderDto>> UpdateAsync(int id, BranchDailyTargetHeaderCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<BranchDailyTargetHeader>();

            var entity = await repo.GetAsync(
                x => x.Id == id,
                include: q => q.Include(h => h.Details)
            );

            if (entity == null)
                return ApiResponse<BranchDailyTargetHeaderDto>.Fail("Target not found");

            entity.BranchId = model.BranchId;
            entity.TargetDate = model.TargetDate;
            entity.TotalBranchTarget = model.TotalBranchTarget;
            entity.TotalAchieved = model.TotalAchieved;

            entity.Details.Clear();
            foreach (var d in model.Details)
                entity.Details.Add(_mapper.Map<BranchDailyTargetDetail>(d));

            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            return await GetByIdAsync(entity.Id);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<BranchDailyTargetHeader>();

            var entity = await repo.GetByIdAsync(id);
            if (entity == null)
                return ApiResponse<bool>.Fail("Target not found");

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<bool>.Ok(true, "Target deleted successfully");
        }
    }
}
