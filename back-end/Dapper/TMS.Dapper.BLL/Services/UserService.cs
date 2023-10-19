using AutoMapper;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Users.CRUD;
using TMS.Dapper.Common.DTOs.Users.Custom;
using TMS.Dapper.Common.Exceptions;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper) { }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            _unitOfWork.Commit();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(int id)
        {
            var user = await GetByIdElseThrowException(id);

            _unitOfWork.Commit();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto user)
        {
            await HandleIfUserWithSameMail(user.Email);

            var mapped = _mapper.Map<User>(user);
            var createdId = await _unitOfWork.UserRepository.CreateAsync(mapped);
            var created = await _unitOfWork.UserRepository.GetByIdAsync(createdId);
            _unitOfWork.Commit();

            return _mapper.Map<UserReadDto>(created);
        }

        public async Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto user)
        {
            await GetByIdElseThrowException(id);
            await HandleIfUserWithSameMail(user.Email);
            
            var mapped = _mapper.Map<User>(user);
            mapped.Id = id;
            
            await _unitOfWork.UserRepository.UpdateAsync(mapped);
            _unitOfWork.Commit();

            return _mapper.Map<UserReadDto>(mapped);
        }

        public async Task DeleteUserAsync(int id)
        {
            var deletedCount = await _unitOfWork.UserRepository.DeleteAsync(id);
            _unitOfWork.Commit();

            if (deletedCount < 1)
            {
                throw new NotFoundException($"User with Id: {id} could not be found.");
            }
        }

        public async Task<IEnumerable<UserWithProjectsDTO>> GetUsersWithProjectsAsync()
        {
            var users = await _unitOfWork.UserRepository.GetUsersWithProjectsAsync();
            var mapped = _mapper.Map<IEnumerable<UserWithProjectsDTO>>(users);
            return mapped;
        }

        private async Task HandleIfUserWithSameMail(string email)
        {
            var usersWithSameEmail = await _unitOfWork.UserRepository.GetUsersByEmail(email);
            if (usersWithSameEmail is not null && usersWithSameEmail.Count() != 0)
            {
                throw new BadRequestException($"There is user with the same Email: {email}");
            }
        }

        private async Task<User> GetByIdElseThrowException(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new NotFoundException($"User with Id: {id} could not be found.");
            }

            return user;
        }
    }
}
