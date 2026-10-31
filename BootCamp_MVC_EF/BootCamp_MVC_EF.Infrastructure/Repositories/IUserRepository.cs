using BootCamp_MVC_EF.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp_MVC_EF.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);


        User? GetByEmail(string email);

        void Add(User user);


        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
    }
}
