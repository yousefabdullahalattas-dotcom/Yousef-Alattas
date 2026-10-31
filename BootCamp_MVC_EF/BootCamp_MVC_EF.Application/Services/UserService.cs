using BootCamp_MVC_EF.Application.Dtos;
using BootCamp_MVC_EF.Application.Services.Base;
using BootCamp_MVC_EF.Domain.Models;
using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp_MVC_EF.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public User? Login(LoginDto loginDto)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(loginDto.Username);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }


        public User? Register(RegisterDto registerDto)
        {
            var existingUserByUsername = _unitOfWork.UserRepository.GetByUsername(registerDto.Username);
            if (existingUserByUsername != null)
            {
                return null;
            }
            var existingUserByEmail = _unitOfWork.UserRepository.GetByEmail(registerDto.Email);
            if (existingUserByEmail != null)
            {
                return null;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Name = registerDto.Name,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword
            };
            _unitOfWork.UserRepository.Add(newUser);

            return newUser;
        }


     
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository
                .GetAllUsersAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                UserName = u.UserName,
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
                UserName = user.UserName,
                Email = user.Email,
                IsLocked = user.IsLocked
            };
        }

        public async Task AddUser(UserCreateDto userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                UserName = userDTO.UserName,
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
            user.UserName = userDTO.UserName;
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
