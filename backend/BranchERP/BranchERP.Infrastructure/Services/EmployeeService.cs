using AutoMapper;
using BranchERP.Application.DTOs.Common;
using BranchERP.Application.DTOs.Employee;
using BranchERP.Application.Interfaces;
using BranchERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BranchERP.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ============================
        // Get All
        // ============================
        public async Task<ApiResponse<IReadOnlyList<EmployeeDto>>> GetAllAsync()
        {
            var repo = _unitOfWork.Repository<Employee>();

            var employees = await repo.GetAllAsync();

            var data = _mapper.Map<IReadOnlyList<EmployeeDto>>(employees);
            return ApiResponse<IReadOnlyList<EmployeeDto>>.Ok(data);
        }

        // ============================
        // Get Paged
        // ============================
        public async Task<ApiResponse<IReadOnlyList<EmployeeDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? search = null)
        {
            var repo = _unitOfWork.Repository<Employee>();

            Expression<Func<Employee, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(search))
                filter = e => e.FullName.Contains(search) || e.EmployeeCode.Contains(search);

            var employees = await repo.GetAllAsync(filter);

            var paged = employees
                .OrderBy(e => e.FullName)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var data = _mapper.Map<IReadOnlyList<EmployeeDto>>(paged);
            return ApiResponse<IReadOnlyList<EmployeeDto>>.Ok(data);
        }

        // ============================
        // Get By Id
        // ============================
        public async Task<ApiResponse<EmployeeDto>> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.Repository<Employee>();

            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<EmployeeDto>.Fail("Employee not found");

            var dto = _mapper.Map<EmployeeDto>(entity);
            return ApiResponse<EmployeeDto>.Ok(dto);
        }

        // ============================
        // Create
        // ============================
        public async Task<ApiResponse<EmployeeDto>> CreateAsync(EmployeeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<Employee>();

            var entity = _mapper.Map<Employee>(model);

            await repo.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<EmployeeDto>(entity);
            return ApiResponse<EmployeeDto>.Ok(dto, "Employee created successfully");
        }

        // ============================
        // Update
        // ============================
        public async Task<ApiResponse<EmployeeDto>> UpdateAsync(int id, EmployeeCreateUpdateDto model)
        {
            var repo = _unitOfWork.Repository<Employee>();

            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<EmployeeDto>.Fail("Employee not found");

            _mapper.Map(model, entity);

            repo.Update(entity);
            await _unitOfWork.CompleteAsync();

            var dto = _mapper.Map<EmployeeDto>(entity);
            return ApiResponse<EmployeeDto>.Ok(dto, "Employee updated successfully");
        }

        // ============================
        // Delete
        // ============================
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var repo = _unitOfWork.Repository<Employee>();

            var entity = await repo.GetByIdAsync(id);

            if (entity is null)
                return ApiResponse<bool>.Fail("Employee not found");

            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();

            return ApiResponse<bool>.Ok(true, "Employee deleted successfully");
        }
    }
}
