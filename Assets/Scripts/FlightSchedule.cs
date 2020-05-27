using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightSchedule 
{
    public Airport Departure;

    public Airport Destination;
    public float TimeToDeparture;

    public FlightSchedule(Airport departure, Airport destination, float time)
    {
        Departure = departure;
        Destination = destination;
        TimeToDeparture = time;
    }

}
