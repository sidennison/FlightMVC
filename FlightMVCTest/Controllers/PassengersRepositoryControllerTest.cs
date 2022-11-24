using FlightMVC.Controllers;
using FlightMVC.Models;
using FlightMVC.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightMVCTest.Controllers
{
    internal class PassengersRepositoryControllerTest
    {
        [Test]
        public void IndexReturnsPassengers()
        {
            Mock<IPassengersRepository> mock = new();
            mock.Setup(o => o.GetPassengers()).Returns(new PassengerDetails[]
            {
                new PassengerDetails(){Name="Fred", Weight=23}
            });

            // Arrange
            var controller = new PassengersRepositoryController(mock.Object);

            // Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<IEnumerable<PassengerDetails>>());
        }
    }
}
