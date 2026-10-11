using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetAllUsersAsync());
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsLocked = user.IsLocked
            };

            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateDto userDTO)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByIdAsync(userDTO.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = userDTO.Name;
                user.Email = userDTO.Email;
                user.IsLocked = userDTO.IsLocked;

                if (!string.IsNullOrEmpty(userDTO.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                }

                await _userRepository.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(userDTO);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDTO)
        {
            if (ModelState.IsValid)
            {

                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                    IsLocked = userDTO.IsLocked
                };

                await _userRepository.AddUserAsync(user);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(userDTO);

        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserUpdateDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsLocked = user.IsLocked
            };

            return View(userDTO);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(user.Id);

            return RedirectToAction("Index");
        }

    }
}