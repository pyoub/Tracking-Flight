using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using vol.Models.Entity;
using vol.Models.Factory;
using vol.Models.Interfaces;

namespace vol.Models.Context
{
    public class DataGenerator
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var factory = 
                serviceProvider.GetRequiredService<DbFactory>();
            
            
                var unitOfWork = new UnitOfWork.UnitOfWork(factory);
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
                unitOfWork.PlaneRepository.Add(plane);
                unitOfWork.AeroportRepository.Add(aeroportArrival);
                unitOfWork.AeroportRepository.Add(aeroportDist);
                unitOfWork.FlightRepository.Add(flight);

                await unitOfWork.CommitAsync().ConfigureAwait(false);
            
        }
    }
}