using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories.Base;
using BootCamp_MVC_EF.Services.Base;

namespace BootCamp_MVC_EF.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository
                .GetAllUsersAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                IsLocked = u.IsLocked
            });
        }

        public async Task<UserUpdateDto?> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsLocked = user.IsLocked
            };
        }

        public async Task AddUser(UserCreateDto userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                IsLocked = userDTO.IsLocked
            };

            await _unitOfWork.UserRepository
                .AddUserAsync(user);
        }

        public async Task UpdateUser(UserUpdateDto userDTO)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByIdAsync(userDTO.Id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.IsLocked = userDTO.IsLocked;

            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }

            await _unitOfWork.UserRepository
                .UpdateUserAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _unitOfWork.UserRepository
                .GetUserByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            await _unitOfWork.UserRepository
                .DeleteUserAsync(id);
        }
    }
}
