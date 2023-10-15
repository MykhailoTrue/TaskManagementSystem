using TMS.Dapper.Common.DTOs.Users.CRUD;

namespace TMS.Dapper.BLL.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();

        Task<UserReadDto> GetUserByIdAsync(int id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto user);
        Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto user);
        Task<bool> DeleteUserAsync(int id);
    }
}
