using vol.Models.Entity;
using vol.Services;

namespace vol.ViewModel
{
    public class FlightView : Flight
    {
        public FlightView(Flight flight)
        {
            Id = flight.Id;
            Arrival = flight.Arrival;
            Destination = flight.Destination;
            ArrivalId = flight.ArrivalId;
            DestinationId = flight.DestinationId;
            Plane = flight.Plane;
            PlaneId = flight.PlaneId;
            CreatedDate = flight.CreatedDate;
            UpdatedDate = flight.UpdatedDate;
            VoleTimeByHours = flight.VoleTimeByHours;
        }
        public double Distance { get => FlightService.CalcDistance(Arrival,Destination);}
        public double KeroseneConsumption {get => FlightService.CalcKerosine(this);}
    }
}