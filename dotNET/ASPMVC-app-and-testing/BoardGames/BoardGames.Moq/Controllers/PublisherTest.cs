using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGames.Controllers;
using BoardGames.Models;
using BoardGames.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace BoardGames.Moq.Controllers {

    [TestClass]
    public class PublisherTest {

        private Mock<IPublishersRepository> _publishersMock;
        private PublishersController _controller;

        [TestInitialize]
        public void Initialize() {
            _publishersMock = new Mock<IPublishersRepository>();
            _controller = new PublishersController(_publishersMock.Object);
        }



        [TestMethod]
        public async Task Index_GivenNoArgs_ReturnsViewResult() {
            var publishers = new List<Publisher>() {
                new Publisher(),
                new Publisher()
            };
            _publishersMock.Setup(p => p.GetPublishers()).ReturnsAsync(publishers);

            var result = await _controller.Index("", "");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public async Task Details_GivenDataIsValid_ReturnsViewResult() {
            var publisher1 = new Publisher() { ID = 1 };
            _publishersMock.Setup(p => p.GetPublisherByID(1)).ReturnsAsync(publisher1);

            var result = await _controller.Details(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Details_GivenDataIsInvalid_ReturnsNotFoundView() {
            // Arrange all in Initialize()

            var result = await _controller.Details(-1) as ViewResult; // ID can't be negative

            Assert.AreEqual("NotFound", result.ViewName);
        }



        [TestMethod]
        public void Create_GivenNoArgs_ReturnsViewResult() {
            // Arrange all in Initalize()

            var result = _controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Create_GivenDataIsValid_ReturnsRedirectToActionResult() {
            var publisher = new Publisher();

            var result = await _controller.Create(publisher);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Create_GivenDataIsInvalid_ReturnsViewResult() {
            var publisher = new Publisher() { ID = -1 };
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.Create(publisher);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }



        [TestMethod]
        public async Task Edit_GivenIdIsValid_ReturnsViewResult() {
            var publisher = new Publisher() { ID = 1 };
            _publishersMock.Setup(p => p.GetPublisherByID(1)).ReturnsAsync(publisher);

            var result = await _controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Edit_GivenIdIsInvalid_ReturnsNotFoundView() {
            // Arrange all in Initalize()

            var result = await _controller.Edit(-1) as ViewResult; // ID can't be negative

            Assert.AreEqual("NotFound", result.ViewName);
        }



        [TestMethod]
        public async Task Edit_GivenDataIsValid_ReturnsRedirectToActionResult() {
            var publisher = new Publisher() { ID = 1 };
            _publishersMock.Setup(p => p.GetPublisherByID(1)).ReturnsAsync(publisher);

            var result = await _controller.Edit(1, publisher); // matching games IDs

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public async Task Edit_GivenDataIsInvalidIDsDontMatch_ReturnsNotFoundView() {
            var publisher = new Publisher() { ID = 2 };
            _publishersMock.Setup(p => p.GetPublisherByID(2)).ReturnsAsync(publisher);
            
            var result = await _controller.Edit(1, publisher) as ViewResult;

            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public async Task Edit_GivenDataIsInvalid_ReturnsViewResult() {
            var publisher = new Publisher() { ID = 1 };
            _publishersMock.Setup(p => p.GetPublisherByID(1)).ReturnsAsync(publisher);
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.Edit(1, publisher) as ViewResult;
            var model = result.Model as Publisher;

            Assert.AreEqual(model, publisher);
        }



        [TestMethod]
        public async Task Delete_GivenDataIsValid_ReturnsViewResult() {
            var publisher = new Publisher() { ID = 1 };
            _publishersMock.Setup(p => p.GetPublisherByID(1)).ReturnsAsync(publisher);

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
            _publishersMock = null;
            _controller = null;
        }
    }
}
