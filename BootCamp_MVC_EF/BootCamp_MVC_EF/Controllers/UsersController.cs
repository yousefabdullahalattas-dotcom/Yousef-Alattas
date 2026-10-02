using BootCamp_MVC_EF.Data;
using BootCamp_MVC_EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<User> users = _db.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(user);
        }

        //Edit---------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var user = _db.Users.Find(Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users.Find(user.Id);

                if (!string.IsNullOrEmpty(user.Password))
                {
                    oldUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                oldUser.Name = user.Name;
                oldUser.Email = user.Email;
                oldUser.IsLocked = user.IsLocked;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(user);
        }

        //Delete-------------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var user = _db.Users.Find(Id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(User user)
        {
            var existingUser = _db.Users.Find(user.Id);
            _db.Users.Remove(existingUser);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
