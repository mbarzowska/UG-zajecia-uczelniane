using System;
using BoardGames.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardGames.Tests.Models {
    
    [TestClass]
    public class GameTest {

        private string _title, _genre;
        private int _minGamers, _maxGamers, _price, _publisherId;
        private DateTime _releaseDate;

        [TestInitialize]
        public void Initialize() {
            _title = "5 sekund";
            _genre = "Familijna";
            _minGamers = 3;
            _maxGamers = 6;
            _price = 30;
            _publisherId = 3;
            _releaseDate = new DateTime(2011, 6, 10);
        }

        [TestMethod]
        public void ValidateModel_GivenValidModel_ExpectNoValidationErrors() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual(0, results.Count); 
        }

        [TestMethod]
        public void ValidateModel_GivenTitleIsNull_ExpectValidationError() {
            var model = new Game() {
                Title = null,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The Title field is required.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenGenreIsNull_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = null,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The Genre field is required.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenTitleIsTooShort_ExpectValidationError() {
            var model = new Game() {
                Title = "5",
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Title must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenTitleIsTooLong_ExpectValidationError() {
            var model = new Game() {
                Title = "5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund " +
                        "5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund " +
                        "5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund 5 sekund ",
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Title must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenGenreIsTooShort_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = "Fa",
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };

            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Genre must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenGenreIsTooLong_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = "Familijna Familijna Familijna Familijna Familijna Familijna Familijna ",
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Genre must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenMinGamersIsTooSmall_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = 0,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Min Gamers must be between 1 and 25.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenMinGamersIsTooBig_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = 26,
                MaxGamers = _maxGamers,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual(3, results.Count);
            //Assert.AreEqual("Check min gamers and max gamers fields, max cannot be less than min.", results[0].ErrorMessage);
            //Assert.AreEqual("The field Min Gamers must be between 1 and 25.", results[1].ErrorMessage);
            //Assert.AreEqual("Check min gamers and max gamers fields, max cannot be less than min.", results[2].ErrorMessage);
        }

        [TestMethod]
        public void ValidateModel_GivenMaxGamersIsTooSmall_ExpectOneVidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = 0,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual(3, results.Count);
            //Assert.AreEqual("Check min gamers and max gamers fields, max cannot be less than min.", results[0].ErrorMessage);
            //Assert.AreEqual("Check min gamers and max gamers fields, max cannot be less than min.", results[1].ErrorMessage);
            //Assert.AreEqual("The field Max Gamers must be between 1 and 25.", results[2].ErrorMessage);
        }

        [TestMethod]
        public void ValidateModel_GivenMaxGamersIsTooBig_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = 26,
                Price = _price,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Max Gamers must be between 1 and 25.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenPriceIsTooSmall_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = 0,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Price must be between 1 and 100.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenPriceIsTooBig_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = 101,
                PublisherId = _publisherId,
                ReleaseDate = _releaseDate
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Price must be between 1 and 100.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenPriceIsTooSmallComparedToRelease_ExpectValidationError() {
            var model = new Game() {
                Title = _title,
                Genre = _genre,
                MinGamers = _minGamers,
                MaxGamers = _maxGamers,
                Price = 20,
                PublisherId = _publisherId,
                ReleaseDate = new DateTime(2018, 04, 16)
            };
            
            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("A rental price of recently released game cannot be less than 50.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestCleanup]
        public void Cleanup() {
            _title = null;
            _genre = null;
            _minGamers = 0;
            _maxGamers = 0;
            _price = 0;
            _publisherId = 0;
        }
    }
}
