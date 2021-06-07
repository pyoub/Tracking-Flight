namespace vol.Models.Entity
{
    public class Plane : EntityBase
    {
        
        public string Name { get; set; }

        public int KeroseneConsumptionByHour { get; set; }

        
        public int TakeOff { get; set; }
    }
}
