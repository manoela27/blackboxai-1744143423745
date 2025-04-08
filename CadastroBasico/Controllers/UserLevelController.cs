using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroBasico.Data;
using CadastroBasico.Models;
using CadastroBasico.DTOs;

namespace CadastroBasico.Controllers
{
    public class UserLevelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLevelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userLevels = await _context.UserLevels
                .Select(ul => new UserLevelDTO
                {
                    Id = ul.Id,
                    Name = ul.Name,
                    Description = ul.Description
                })
                .ToListAsync();

            return View(userLevels);
        }

        public IActionResult Create()
        {
            return View(new UserLevelDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserLevelDTO userLevelDTO)
        {
            if (ModelState.IsValid)
            {
                var userLevel = new UserLevel
                {
                    Name = userLevelDTO.Name,
                    Description = userLevelDTO.Description
                };

                _context.Add(userLevel);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Nível de usuário criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(userLevelDTO);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLevel = await _context.UserLevels.FindAsync(id);
            if (userLevel == null)
            {
                return NotFound();
            }

            var userLevelDTO = new UserLevelDTO
            {
                Id = userLevel.Id,
                Name = userLevel.Name,
                Description = userLevel.Description
            };

            return View(userLevelDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserLevelDTO userLevelDTO)
        {
            if (id != userLevelDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userLevel = await _context.UserLevels.FindAsync(id);
                    if (userLevel == null)
                    {
                        return NotFound();
                    }

                    userLevel.Name = userLevelDTO.Name;
                    userLevel.Description = userLevelDTO.Description;

                    _context.Update(userLevel);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Nível de usuário atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLevelExists(userLevelDTO.Id))
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
            return View(userLevelDTO);
        }

        private bool UserLevelExists(int id)
        {
            return _context.UserLevels.Any(e => e.Id == id);
        }
    }
}
