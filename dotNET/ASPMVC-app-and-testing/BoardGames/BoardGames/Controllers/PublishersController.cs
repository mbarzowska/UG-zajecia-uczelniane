using System;
using System.Linq;
using System.Threading.Tasks;
using BoardGames.Models;
using BoardGames.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersRepository _publishersRepository;

        public PublishersController(IPublishersRepository publishersRepository)
        {
            _publishersRepository = publishersRepository;
        }

        // GET: Publishers
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(string sortOrder, string searchString) {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            var publishers = await _publishersRepository.GetPublishers();

            if (!String.IsNullOrEmpty(searchString)) {
                publishers = publishers.Where(s => s.CompanyName.Contains(searchString));
            }
            switch (sortOrder) {
                case "name_desc":
                    publishers = publishers.OrderByDescending(s => s.CompanyName);
                    break;
                case "Date":
                    publishers = publishers.OrderBy(s => s.FoundingDate);
                    break;
                case "date_desc":
                    publishers = publishers.OrderByDescending(s => s.FoundingDate);
                    break;
                default:
                    publishers = publishers.OrderBy(s => s.CompanyName);
                    break;
            }
         //       return View(await publishers.AsNoTracking().ToListAsync());
         return View(publishers);
        }

        // POST: Publishers
        [HttpPost]
        public string Index(string searchString, bool notUsed) {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Publishers/Details/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int id) {
            var publisher = await _publishersRepository.GetPublisherByID(id);

            if (publisher == null) {
                return View("NotFound");
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompanyName,FoundingDate,CountryOfOrigin,Telephone")] Publisher publisher) {
            if (ModelState.IsValid) {
                _publishersRepository.AddPublisher(publisher);
                await _publishersRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id) {
            var publisher = await _publishersRepository.GetPublisherByID(id);
            if (publisher == null) {
                return View("NotFound");
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CompanyName,FoundingDate,CountryOfOrigin,Telephone")] Publisher publisher) {
            if (id != publisher.ID) {
                return View("NotFound");
            }

            if (ModelState.IsValid) {
                try {
                    _publishersRepository.UpdatePublisher(publisher);
                    await _publishersRepository.Save();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!PublisherExists(publisher.ID)) {
                        return View("NotFound");
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) {
            var publisher = await _publishersRepository.GetPublisherByID(id);
            if (publisher == null) {
                return View("NotFound");
            }
            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            _publishersRepository.DeletePublisher(id);
            await _publishersRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id) {
            return _publishersRepository.PublisherExists(id);
        }
    }
}
