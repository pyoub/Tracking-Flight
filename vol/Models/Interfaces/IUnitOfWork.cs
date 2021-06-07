using System;
using System.Threading.Tasks;
using vol.Models.Entity;

namespace vol.Models.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Flight> FlightRepository {get; init;}
        IRepository<Plane> PlaneRepository {get; init;}
        IRepository<Aeroport> AeroportRepository {get; init;}

        Task CommitAsync();
    }
}