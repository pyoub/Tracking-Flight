using System.ComponentModel.DataAnnotations.Schema;

namespace vol.Models.Entity
{
    public class Flight : EntityBase
    {

        public long PlaneId { get; set; }
        
        public long DestinationId { get; set; }

        public long ArrivalId { get; set; }

        public int VoleTimeByHours { get; set; }
        
        [ForeignKey(nameof(DestinationId))]   
        public virtual Aeroport Destination { get; set; }

        [ForeignKey(nameof(ArrivalId))]   
        public virtual Aeroport Arrival { get; set; }

        [ForeignKey(nameof(PlaneId))]
        public virtual Plane Plane { get; set; }

    }
}
