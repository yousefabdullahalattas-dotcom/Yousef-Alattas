using BootCamp_MVC_EF.Application.Dtos;
using BootCamp_MVC_EF.Domain.Models;
using BootCamp_MVC_EF.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp_MVC_EF.Application.Services.Base
{
    public interface IUserService
    {
        User? Login(LoginDto loginDto);

        User? Register(RegisterDto registerDto);


        Task<IEnumerable<UserDto>> GetAllUsers();

        Task<UserUpdateDto?> GetUserById(int id);

        Task AddUser(UserCreateDto userDTO);

        Task UpdateUser(UserUpdateDto userDTO);

        Task DeleteUser(int id);
    }
}
