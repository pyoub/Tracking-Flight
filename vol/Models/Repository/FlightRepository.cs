using vol.Models.Context;
using vol.Models.Entity;
using vol.Models.Factory;

namespace vol.Models.Repository
{
    public sealed class FlightRepository : Repository<Flight>
    {
        public FlightRepository(DbFactory dbFactory):base(dbFactory)
        {
            
        }
    }
}