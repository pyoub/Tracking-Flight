namespace vol.Models.Entity
{
    public class Aeroport : EntityBase
    {
        public string Name { get; set; }

        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
    }
}