using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();

            return View(users);
        }

        public IActionResult Create()
        {
            return View(new UserCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateDto userDTO)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddUser(userDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(userDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto userDTO)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var existingUser =
                    await _userService.GetUserById(userDTO.Id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                await _userService.UpdateUser(userDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(userDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}
