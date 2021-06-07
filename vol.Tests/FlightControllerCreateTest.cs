using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using vol.Controllers;
using vol.Models.Context;
using vol.Models.Entity;
using vol.Models.Interfaces;
using Xunit;

namespace vol.Tests
{
    public class FlightControllerCreateTest
    {
        private IMock<IUnitOfWork> mockedUnitOfWork = new Mock<IUnitOfWork>();

        [Fact]
        public async Task Create_WhenDataValid_GoodResult()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var controller = new FlightController(mockedUnitOfWork.Object);
            var plane = new Plane{
                        Name = "boeing 747",
                        KeroseneConsumptionByHour = 10,
                        TakeOff = 100,
                        CreatedDate = DateTime.Now
                    };
                
                var aeroportArrival = new Aeroport{
                    CreatedDate = new DateTime(),
                    Latitude = 33.3698285,
                    Longitude = -7.5895013,
                    Name = "Casablanca Mohammed V International Airport"
                };

                var aeroportDist = new Aeroport{
                    CreatedDate = new DateTime(),
                    Latitude = 30.3299334,
                    Longitude = -9.4124746,
                    Name = "Agadir Al-Massira International Airport"
                };

                var flight = new Flight{
                    Arrival = aeroportArrival,
                    Destination = aeroportDist,
                    CreatedDate = DateTime.Now,
                    Plane = plane,
                    VoleTimeByHours = 4
                };
            mockedUnitOfWork.Setup(_ => _.CommitAsync()).Verifiable();
            mockedUnitOfWork.Setup(_ => _.FlightRepository.Add(It.IsAny<Flight>())).Verifiable();

            var result = await controller.Create(flight).ConfigureAwait(false);
            
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
