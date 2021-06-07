using vol.Models.Entity;
using vol.Models.Factory;

namespace vol.Models.Repository
{
    public sealed class PlaneRepository : Repository<Plane>
    {
        public PlaneRepository(DbFactory dbFactory):base(dbFactory)
        {
            
        }
    }
}