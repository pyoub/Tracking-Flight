using vol.Models.Entity;
using vol.Models.Factory;

namespace vol.Models.Repository
{
    public sealed class AeroportRepository : Repository<Aeroport>
    {
        public AeroportRepository(DbFactory dbFactory):base(dbFactory)
        {
            
        }
    }
}