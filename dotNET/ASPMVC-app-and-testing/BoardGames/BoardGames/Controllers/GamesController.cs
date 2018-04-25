using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;
using BoardGames.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Controllers {
    public class GamesController : Controller {
        private readonly IPublishersRepository _publishersRepository;
        private readonly IGamesRepository _gamesRepository;

        public GamesController(IGamesRepository gamesRepository, IPublishersRepository publishersRepository) {
            _gamesRepository = gamesRepository;
            _publishersRepository = publishersRepository;
        }

        // GET: Games
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(string gameGenre, string searchString, string sortOrder) {
            var games = await _gamesRepository.GetGames();


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["MingamersSortParm"] = sortOrder == "MinGamers" ? "ming_desc" : "MinGamers";
            ViewData["MaxgamersSortParm"] = sortOrder == "MaxGamers" ? "maxg_desc" : "MaxGamers";

            if (!String.IsNullOrEmpty(searchString)) {
                games = games.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder) {
                case "name_desc":
                    games = games.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "MinGamers":
                    games = games.OrderBy(s => s.MinGamers);
                    break;
                case "ming_desc":
                    games = games.OrderByDescending(s => s.MinGamers);
                    break;
                case "MaxGamers":
                    games = games.OrderBy(s => s.MaxGamers);
                    break;
                case "maxg_desc":
                    games = games.OrderByDescending(s => s.MaxGamers);
                    break;
                default:
                    games = games.OrderBy(s => s.Title);
                    break;
            }

            return View(games);
        }

        // POST: Games
        [HttpPost]
        public string Index(string searchString, bool notUsed) {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Games/Details/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int id) {
            var game = await _gamesRepository.GetGameByID(id);
            if (game == null) {
                return View("NotFound");
            }

            return View(game);
        }

        // GET: Games/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create() {
            ViewData["PublisherId"] = new SelectList(await _publishersRepository.GetPublishers(), "ID", "CompanyName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Genre,MinGamers,MaxGamers,ReleaseDate,Price,PublisherId")] Game game) {
            if (ModelState.IsValid) {
                _gamesRepository.AddGame(game);
                await _gamesRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(await _publishersRepository.GetPublishers(), "ID", "CompanyName", game.PublisherId);
            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id) {
            var game = await _gamesRepository.GetGameByID(id);
            if (game == null) {
                return View("NotFound");
            }
            ViewData["PublisherId"] = new SelectList(await _publishersRepository.GetPublishers(), "ID", "CompanyName", game.PublisherId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Genre,MinGamers,MaxGamers,ReleaseDate,Price,PublisherId")] Game game) {
            if (id != game.ID) {
                return View("NotFound");
            }

            if (ModelState.IsValid) {
                try {
                    _gamesRepository.UpdateGame(game);
                    await _gamesRepository.Save();
                } catch (DbUpdateConcurrencyException) {
                    if (!GameExists(game.ID)) {
                        return View("NotFound");
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublisherId"] = new SelectList(await _publishersRepository.GetPublishers(), "ID", "CompanyName", game.PublisherId);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) {
            var game = await _gamesRepository.GetGameByID(id);
            if (game == null) {
                return View("NotFound");
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            _gamesRepository.DeleteGame(id);
            await _gamesRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id) {
            return _gamesRepository.GameExists(id);
        }
    }
}
