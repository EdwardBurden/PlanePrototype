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
    private float MinFlightInterval;


    public Wave(GameManger manger)
    {
        WaveNumber = 0;
        ActiveAirports = new List<Airport>();
        AirportNumber = manger.Settings.StartingAirportNumber;
        FlightNumber = manger.Settings.StartingFlightPerAirport;
        MaxFlightInterval = manger.Settings.MaxFlightTimeInterval;
        MinFlightInterval = manger.Settings.MinFlightTimeInterval;
        Debug.Log("Max:" + MaxFlightInterval + "Min:" + MinFlightInterval);
        foreach (Airport airport in GameManger.Instance.AirPorts)
        {
            airport.AirportEmptied += AirportEmpty;
        }
        ActiveAirports = GameManger.Instance.AirPorts.OrderBy(x => x.AppearanceOrder).Take(Mathf.FloorToInt(AirportNumber)).ToList();
        ScheuleFlights();
    }

    public void NextWave()
    {
        WaveNumber++;
        AirportNumber += GameManger.Instance.Settings.AirportIncreasePerWave;
        FlightNumber += GameManger.Instance.Settings.FlightIncreasePerWave;
        MaxFlightInterval -= GameManger.Instance.Settings.FlightTimeIntervalAcceleration;
        if (MaxFlightInterval < MinFlightInterval)
            MaxFlightInterval = MinFlightInterval;
        Debug.Log("Max:" + MaxFlightInterval + "Min:" + MinFlightInterval);
        ActiveAirports = GameManger.Instance.AirPorts.OrderBy(x => x.AppearanceOrder).Take(Mathf.FloorToInt(AirportNumber)).ToList();
        ScheuleFlights();
    }

    private void ScheuleFlights()
    {
        foreach (Airport airportStart in ActiveAirports)
        {
            float timetotal = 0;
            List<FlightSchedule> flights = new List<FlightSchedule>();
            for (int i = 0; i < Mathf.FloorToInt(FlightNumber); i++)
            {
                List<Airport> available = ActiveAirports.Where(x => x != airportStart).ToList();
                timetotal += Random.Range(MinFlightInterval, MaxFlightInterval);
                int index = Random.Range(0, available.Count);

                FlightSchedule flight = new FlightSchedule(airportStart, available[index], timetotal);
                flights.Add(flight);
            }
            airportStart.SetFlights(flights);
        }

    }

    private void AirportEmpty()
    {
        if (ActiveAirports.All(x => !x.FlightsActive))
        {
            NextWave();
            CameraControl.Instance.MoveCamera();
        }
    }
}
