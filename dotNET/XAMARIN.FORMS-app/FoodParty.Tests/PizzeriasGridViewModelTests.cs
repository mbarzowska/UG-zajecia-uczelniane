using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FoodParty.Models;
using FoodParty.Repositories;
using FoodParty.Validators;
using FoodParty.ViewModels;
using Moq;
using Xunit;

namespace FoodParty.Tests
{
    public class PizzeriasGridViewModelTests
    {
        [Fact]
        public void RefreshCommand_RetursList() 
        {
            // Arrange
            var pizzeriaRepositoryMock = new Mock<IPizzeriaRepository>();
            var pizzerias = new List<Pizzeria> {
                new Pizzeria { Name = "Monika" },
                new Pizzeria { Name = "Pizza Hut" }
            };

            pizzeriaRepositoryMock.Setup(x => x.GetPizzeriasAsync()).ReturnsAsync(pizzerias);
            var vm = new PizzeriasGridViewModel { PizzeriaRepository = pizzeriaRepositoryMock.Object };

            // Act
            vm.RefreshCommand.Execute(null);

            // Assert
            Assert.Equal(2, vm.Pizzerias.Count);
        }

        [Fact]
        public void AddPizzeriaCommand_AddsPizzeriaToRepository() 
        {
            // Arrange
            var pizzeriaRepositoryMock = new Mock<IPizzeriaRepository>();
            var vm = new PizzeriasGridViewModel {
                PizzeriaRepository = pizzeriaRepositoryMock.Object,
                PizzeriaName = "Pizza Hut",
                PizzeriaPhotoURL = string.Empty
            };

            // Act
            vm.AddCommand.Execute(null);

            // Assert
            // https://stackoverflow.com/questions/9136674/verify-a-method-call-using-moq
            pizzeriaRepositoryMock.Verify(x => x.AddPizzeriaAsync(It.IsAny<Pizzeria>()), Times.Exactly(1));
        }

        [Fact]
        public void SettingPizzerias_FiresProperyChangedEvent()
        // https://stackoverflow.com/questions/45944721/how-to-unit-test-a-property-changed-event
        // https://jeremybytes.blogspot.com/2015/07/tracking-property-changes-in-unit-tests.html
        {
            // Arrange
            var vm = new CompareViewModel();
            bool eventRaised = false;
            vm.PropertyChanged += (sender, e) => eventRaised = true;

            // Act
            vm.Pizzerias = new ObservableCollection<Pizzeria>();

            // Assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void SettingPizzeriasMultipleTimes_FiresProperyChangedEvent_ExpectedTimes() 
        {
            // Arrange
            var vm = new CompareViewModel();
            int timesFired = 0;
            vm.PropertyChanged += (sender, e) => timesFired++;

            // Act
            vm.Pizzerias = new ObservableCollection<Pizzeria>();
            vm.Pizzerias = new ObservableCollection<Pizzeria>();
            vm.Pizzerias = new ObservableCollection<Pizzeria>();

            // Assert
            Assert.Equal(3, timesFired);
        }
        
        [Fact]
        public void SwitchingMode_SwitchesMode() 
        {
            // Arrange
            var vm = new PizzeriasGridViewModel { IsGridMode = false };

            // Act
            vm.SwitchMode.Execute(null);

            // Assert
            Assert.True(vm.IsGridMode);
        }
    }
}
