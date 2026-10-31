using BootCamp_MVC_EF.Dtos;

namespace BootCamp_MVC_EF.Services.Base
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserUpdateDto?> GetUserById(int id);

        Task AddUser(UserCreateDto userDTO);

        Task UpdateUser(UserUpdateDto userDTO);

        Task DeleteUser(int id);
    }
}
