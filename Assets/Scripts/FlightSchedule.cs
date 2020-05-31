using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSchedule 
{
    public Airport Departure;
    public Airport Destination;
    public float TimeToDeparture;
    public int Passengers;
    public int Fuel;

    public FlightSchedule(Airport departure, Airport destination, float timeToDeparture, int passengers, int fuel)
    {
        Departure = departure;
        Destination = destination;
        TimeToDeparture = timeToDeparture;
        Passengers = passengers;
        Fuel = fuel;
    }
}
