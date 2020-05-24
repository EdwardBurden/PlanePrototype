using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings")]
public class GameManagerSettings : ScriptableObject
{
    public Plane PlanePrefab; 
    
    public int StartingAirportNumber;
    public float AirportIncreasePerWave;

    public int StartingFlightPerAirport;
    public float FlightIncreasePerWave;

    public float MaxFlightTimeInterval;
    public float MinFlightTimeInterval;
    public float FlightTimeIntervalAcceleration;
}
