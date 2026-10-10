using BootCamp_MVC_EF.Models;
using BootCamp_MVC_EF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public async Task<IActionResult> Details(int? id)
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

            return View(user);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,IsLocked")] User user)
        {
            if (ModelState.IsValid)
            {
  
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _userRepository.AddUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Email,Password,IsLocked")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }


            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    var oldUser = await _userRepository.GetUserByIdAsync(user.Id);
                    if (oldUser == null)
                    {
                        return NotFound();
                    }

                    oldUser.Name = user.Name;
                    oldUser.Email = user.Email;
                    oldUser.IsLocked = user.IsLocked;


                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    }

                    await _userRepository.UpdateUserAsync(oldUser);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var user = await _userRepository.GetUserByIdAsync(id.Value);
            if (user != null)
            {
                await _userRepository.DeleteUserAsync(user.Id);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}