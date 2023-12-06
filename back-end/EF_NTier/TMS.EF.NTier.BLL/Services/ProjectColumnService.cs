using AutoMapper;
using TMS.EF.NTier.BLL.Services.Interfaces;
using TMS.EF.NTier.Common.DTO.ProjectColumns;
using TMS.EF.NTier.Common.Exceptions;
using TMS.EF.NTier.DAL.Entities;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.BLL.Services
{
    public class ProjectColumnService : BaseService, IProjectColumnService
    {
        public ProjectColumnService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ProjectColumnReadDTO> CreateProjectColumnAsync(ProjectColumnCreateDTO projectColumn)
        {
            var mapped = _mapper.Map<ProjectColumn>(projectColumn);

            _unitOfWork.ProjectColumnRepository.CreateProjectColumn(mapped);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProjectColumnReadDTO>(mapped);
        }

        public async Task DeleteProjectColumnAsync(int id)
        {
            var projectColumn = await _unitOfWork.ProjectColumnRepository.GetProjectColumnByIdAsync(id);
            if (projectColumn is null)
            {
                throw new NotFoundException($"Project Column with Id: {id} could not be found");
            }

            _unitOfWork.ProjectColumnRepository.Delete(projectColumn);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<ProjectColumnReadDTO>> GetAllProjectColumnsAsync()
        {
            var projectColumns = await _unitOfWork.ProjectColumnRepository.GetAllProjectColumnsAsync();

            var mapped = _mapper.Map<IEnumerable<ProjectColumnReadDTO>>(projectColumns);
            return mapped;
        }

        public async Task<ProjectColumnReadDTO> GetProjectColumnByIdAsync(int id)
        {
            var projectColumn = await _unitOfWork.ProjectColumnRepository.GetProjectColumnByIdAsync(id);

            if (projectColumn is null)
            {
                throw new NotFoundException($"Project Column with Id: {id} could not be found");
            }

            return _mapper.Map<ProjectColumnReadDTO>(projectColumn);
        }

        public async Task<ProjectColumnWithIssuesDTO> GetProjectColumnWithDetailsAsync(int id)
        {
            var projectColumn = await _unitOfWork.ProjectColumnRepository.GetProjectColumnWithDetailsAsync(id);

            if (projectColumn is null)
            {
                throw new NotFoundException($"Project Column with Id: {id} could not be found");
            }

            return _mapper.Map<ProjectColumnWithIssuesDTO>(projectColumn);
        }

        public async Task<ProjectColumnReadDTO> UpdateProjectColumnAsync(int id, ProjectColumnUpdateDTO projectColumn)
        {
            var existed = await _unitOfWork.ProjectColumnRepository.GetProjectColumnByIdAsync(id);
            if (existed is null)
            {
                throw new NotFoundException($"Project Column with Id: {id} could not be found");
            }

            existed.Name = projectColumn.Name;
            existed.ProjectId = projectColumn.ProjectId;
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProjectColumnReadDTO>(existed);
        }
    }
}
