using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroBasico.Data;
using CadastroBasico.Models;
using CadastroBasico.DTOs;

namespace CadastroBasico.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .Include(u => u.UserLevel)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Active = u.Active,
                    UserLevelId = u.UserLevelId,
                    UserLevelName = u.UserLevel.Name
                })
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.UserLevels = await _context.UserLevels.ToListAsync();
            return View(new UserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = userDTO.Password, // Em produção, deve-se usar hash
                    UserLevelId = userDTO.UserLevelId,
                    Active = userDTO.Active
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.UserLevels = await _context.UserLevels.ToListAsync();
            return View(userDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Active = user.Active,
                UserLevelId = user.UserLevelId
            };

            ViewBag.UserLevels = await _context.UserLevels.ToListAsync();
            return View(userDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.Users.FindAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.Name = userDTO.Name;
                    user.Email = userDTO.Email;
                    if (!string.IsNullOrEmpty(userDTO.Password))
                    {
                        user.Password = userDTO.Password; // Em produção, deve-se usar hash
                    }
                    user.UserLevelId = userDTO.UserLevelId;
                    user.Active = userDTO.Active;

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.UserLevels = await _context.UserLevels.ToListAsync();
            return View(userDTO);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
