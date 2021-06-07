using System;
using GeoCoordinatePortable;
using vol.Models.Entity;

namespace vol.Services
{
    public static class FlightService
    {
        public static double CalcDistance(Aeroport arrival,Aeroport destination ){
            if(arrival is null || destination is null)
                return 0;
            var arr = new GeoCoordinate(arrival.Latitude,arrival.Longitude);
            var dest = new GeoCoordinate(destination.Latitude,destination.Longitude);
            return Math.Round(arr.GetDistanceTo(dest)/1000,2);
        }

        public static double CalcKerosine(Flight flight){
            if(flight?.Plane is null)
                return 0;
            double fuel =(flight.VoleTimeByHours * (double)flight.Plane.KeroseneConsumptionByHour) + flight.Plane.TakeOff ; 
            return fuel;
        }
    }
}