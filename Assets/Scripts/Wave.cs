using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wave
{
    public int WaveNumber;
    public List<Airport> ActiveAirports;

    private float AirportNumber;
    private float FlightNumber;
    private float MaxFlightInterval;

    private GameManagerSettings Settings;
    private List<Airport> GlobalAirports;
    public List<FlightSchedule> AllFlights;

    public Wave(GameManger manger)
    {
        AllFlights = new List<FlightSchedule>();
        Settings = manger.Settings;
        GlobalAirports = manger.AirPorts;
        WaveNumber = 0;
        ActiveAirports = new List<Airport>();
        AirportNumber = Settings.StartingAirportNumber;
        FlightNumber = Settings.StartingFlightPerAirport;
        MaxFlightInterval = Settings.MaxFlightTimeInterval;
        foreach (Airport airport in GlobalAirports)
        {
            airport.AirportEmptied += manger.AirportEmpty;
        }
        ActiveAirports = GlobalAirports.OrderBy(x => x.AppearanceOrder).Take(Mathf.FloorToInt(AirportNumber)).ToList();
      //  ScheuleFlights();
    }

    public void NextWave()
    {
        WaveNumber++;
        AirportNumber += Settings.AirportIncreasePerWave;
        FlightNumber += Settings.FlightIncreasePerWave;
        MaxFlightInterval -= Settings.FlightTimeIntervalAcceleration;
        MaxFlightInterval = Mathf.Clamp(MaxFlightInterval - Settings.FlightTimeIntervalAcceleration, Settings.MinFlightTimeInterval, Settings.MaxFlightTimeInterval);
        ActiveAirports = GlobalAirports.OrderBy(x => x.AppearanceOrder).Take(Mathf.FloorToInt(AirportNumber)).ToList();
       // ScheuleFlights();
    }

    public void ScheuleFlights()
    {
        AllFlights = new List<FlightSchedule>();
        foreach (Airport airportStart in ActiveAirports)
        {
            float timetotal = 0;
            List<FlightSchedule> flights = new List<FlightSchedule>();
            for (int i = 0; i < Mathf.FloorToInt(FlightNumber); i++)
            {
                List<Airport> available = ActiveAirports.Where(x => x != airportStart).ToList();
                timetotal += Random.Range(Settings.MinFlightTimeInterval, MaxFlightInterval);
                int index = Random.Range(0, available.Count);
                int passengers = Random.Range(Settings.MinPassengers, Settings.MaxPassengers);
                int fuel = Random.Range(Settings.MinFuel, Settings.MaxFuel);
                FlightSchedule flight = new FlightSchedule(airportStart, available[index], timetotal, passengers, fuel);
                flights.Add(flight);

            }
            airportStart.SetFlights(flights);
            AllFlights.AddRange(flights);
        }


    }

   
}
