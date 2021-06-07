using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vol.Models.Context;
using vol.Models.Entity;
using vol.Models.Factory;
using vol.Models.Interfaces;
using vol.Models.Repository;

namespace vol.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbFactory _dbFactory;   

        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            FlightRepository = new FlightRepository(_dbFactory);
            PlaneRepository = new PlaneRepository(_dbFactory);
            AeroportRepository = new AeroportRepository(_dbFactory);
        }

        public  IRepository<Flight> FlightRepository { get; init; }
        public IRepository<Plane> PlaneRepository { get ; init ; }
        public IRepository<Aeroport> AeroportRepository { get ; init; }

        public async Task CommitAsync()
        {
            await _dbFactory.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}