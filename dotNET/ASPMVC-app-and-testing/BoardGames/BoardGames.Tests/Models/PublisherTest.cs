using System;
using BoardGames.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BoardGames.Tests.Models {
    [TestClass]
    public class PublisherTest {

        private string _companyName, _countryOfOrigin, _telephone;
        private DateTime _foundingDate;

        [TestInitialize]
        public void Initialize() {
            _companyName = "Trefl";
            _foundingDate = new DateTime(2011, 6, 10);
            _countryOfOrigin = "Poland";
            _telephone = "555-345-235";
        }

        [TestMethod]
        public void ValidateModel_GivenValidModel_ExpectNoValidationErrors() {
            var model = new Publisher() { 
                CompanyName = _companyName,
                FoundingDate = _foundingDate,
                CountryOfOrigin = _countryOfOrigin,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCompanyNameIsNull_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = null,
                FoundingDate = _foundingDate,
                CountryOfOrigin = _countryOfOrigin,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The Company Name field is required.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCountryOfOriginIsNull_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = _companyName,
                FoundingDate = _foundingDate,
                CountryOfOrigin = null,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The Country Of Origin field is required.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenTelephoneIsNull_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = _companyName,
                FoundingDate = _foundingDate,
                CountryOfOrigin = _countryOfOrigin,
                Telephone = null
            };

            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The Telephone field is required.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCompanyNameIsTooShort_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = "Tr",
                FoundingDate = _foundingDate,
                CountryOfOrigin = _countryOfOrigin,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual("The field Company Name must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCompanyNameIsTooLong_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = "Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl Trefl",
                FoundingDate = _foundingDate,
                CountryOfOrigin = _countryOfOrigin,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual("The field Company Name must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenFoundingDateIsInTheFuture_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = _companyName,
                FoundingDate = new DateTime(2038, 4, 30),
                CountryOfOrigin = _countryOfOrigin,
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual("Check Founding Date field, the date of founding cannot be placed in the future.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCountryOfOriginIsTooShort_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = _companyName,
                FoundingDate = _foundingDate,
                CountryOfOrigin = "Po",
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);

            Assert.AreEqual("The field Country Of Origin must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void ValidateModel_GivenCountryOfOriginIsTooLong_ExpectValidationError() {
            var model = new Publisher() {
                CompanyName = _companyName,
                FoundingDate = _foundingDate,
                CountryOfOrigin = "Poland Poland Poland Poland Poland Poland Poland Poland Poland Poland Poland",
                Telephone = _telephone
            };

            var results = TestModelHelper.Validate(model);
            
            Assert.AreEqual("The field Country Of Origin must be a string with a minimum length of 3 and a maximum length of 60.", results[0].ErrorMessage);
            //Assert.AreEqual(1, results.Count);
        }

        [TestCleanup]
        public void Cleanup() {
            _companyName = null;
            _countryOfOrigin = null;
            _telephone = null;
        }
    }
}
