using AutoMapper;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Projects.CRUD;
using TMS.Dapper.Common.DTOs.Projects.Custom;
using TMS.Dapper.Common.Exceptions;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.BLL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ProjectReadDTO>> GetAllProjectsAsync()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllWithCategoryAsync();
            _unitOfWork.Commit();

            var mapped = _mapper.Map<IEnumerable<ProjectReadDTO>>(projects);

            return mapped;
        }

        public async Task<ProjectReadDTO> GetProjectByIdAsync(int id)
        {
            var project = await GetByIdElseThrowException(id);

            _unitOfWork.Commit();

            return _mapper.Map<ProjectReadDTO>(project);
        }

        public async Task<ProjectReadDTO> CreateProjectAsync(ProjectCreateDTO project)
        {
            var mapped = _mapper.Map<Project>(project);

            var createdId = await _unitOfWork.ProjectRepository.CreateAsync(mapped);
            var created = await _unitOfWork.ProjectRepository.GetByIdWithCategoryAsync(createdId);
            _unitOfWork.Commit();

            return _mapper.Map<ProjectReadDTO>(created);
        }

        public async Task<ProjectReadDTO> UpdateProjectAsync(int id, ProjectUpdateDTO project)
        {
            await GetByIdElseThrowException(id);

            var mapped = _mapper.Map<Project>(project);
            mapped.Id = id;

            await _unitOfWork.ProjectRepository.UpdateAsync(mapped);
            _unitOfWork.Commit();

            return _mapper.Map<ProjectReadDTO>(mapped);
        }

        public async Task DeleteProjectAsync(int id)
        {
            var deletedCount = await _unitOfWork.ProjectRepository.DeleteAsync(id);
            _unitOfWork.Commit();

            if (deletedCount < 1)
            {
                throw new NotFoundException($"Project with Id: {id} could not be found.");
            }
        }

        public async Task<ProjectWithMembersDTO> GetProjectWithMembersAsync(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectWithMembersAsync(id);
            if (project is null)
            {
                throw new NotFoundException($"Project with Id: {id} could not be found");
            }

            var mapped = _mapper.Map<ProjectWithMembersDTO>(project);
            return mapped;
        }

        private async Task<Project> GetByIdElseThrowException(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdWithCategoryAsync(id);
            if (project is null)
            {
                throw new NotFoundException($"Project with Id: {id} could not be found");
            }

            return project;
        }
    }
}
