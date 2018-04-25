using System.Threading.Tasks;
using BoardGames.Controllers;
using BoardGames.Models;
using BoardGames.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardGames.Tests.Controllers {
    
    [TestClass]
    public class GameTest {

        private FakePublishersRepository _publishersRepository;
        private FakeGamesRepository _gamesRepository;
        private GamesController _controller;

        [TestInitialize]
        public void Initialize() {
            _publishersRepository = new FakePublishersRepository();
            _gamesRepository = new FakeGamesRepository();
            _controller = new GamesController(_gamesRepository, _publishersRepository);
        }



        [TestMethod]
        public async Task Index_GivenNoArgs_ReturnsViewResult() {
            // Arrange all in Initialize()

            var result = await _controller.Index("", "", "");
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public async Task Details_GivenDataIsValid_ReturnsViewResult() {
            var game = new Game() { ID = 1 };

            var result = await _controller.Details(game.ID);
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Details_GivenDataIsInvalid_ReturnsNotFoundView() {
            // Arrange all in Initialize()
            
            var result = await _controller.Details(-1) as ViewResult; // ID can't be negative
            
            Assert.AreEqual("NotFound", result.ViewName);
        }



        [TestMethod]
        public async Task Create_GivenNoArgs_ReturnsViewResult() {
            // Arrange all in Initalize()

            var result = await _controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_GivenDataIsValid_ReturnsRedirectToActionResult() {
            var game = new Game();

            var result = await _controller.Create(game);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Create_GivenDataIsInvalid_ReturnsViewResult() {
            var game = new Game();
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.Create(game);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public async Task Edit_GivenIdIsValid_ReturnsViewResult() {
            var game = new Game() { ID = 1 };

            var result = await _controller.Edit(1);
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_GivenIdIsInvalid_ReturnsNotFoundView() {
            // Arrange all in Initialize()
            
            var result = await _controller.Edit(-1) as ViewResult; // ID can't be negative
            
            Assert.AreEqual("NotFound", result.ViewName);
        }



        [TestMethod]
        public async Task Edit_GivenDataIsValid_ReturnsRedirectToActionResult() {
            var game = new Game() { ID = 1 };
            
            var result = await _controller.Edit(1, game); // matching games IDs
            
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_GivenDataIsInvalidIDsDontMatch_ReturnsNotFoundView() {
            var game = new Game() { ID = 2 };
            
            var result = await _controller.Edit(1, game) as ViewResult;
            
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public async Task Edit_GivenDataIsInvalid_ReturnsViewResult() {
            var game = new Game() { ID = 1 };
            _controller.ModelState.AddModelError("", "");
            
            var result = await _controller.Edit(1, game);
            var model = ((ViewResult) result).Model as Game;
            
            Assert.AreEqual(model, game);
            //Assert.IsInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public async Task Delete_GivenDataIsValid_ReturnsViewResult() {
            var game = new Game() { ID = 1 };

            var result = await _controller.Delete(1);
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Delete_GivenDataIsInvalid_ReturnsNotFoundView() {
            // Arrange all in Initialize()
            
            var result = await _controller.Delete(-1) as ViewResult; // ID can't be negative
            
            Assert.AreEqual("NotFound", result.ViewName);
        }



        [TestCleanup]
        public void Cleanup() {
            _publishersRepository = null;
            _gamesRepository = null;
            _controller = null;
        }
    }
}
