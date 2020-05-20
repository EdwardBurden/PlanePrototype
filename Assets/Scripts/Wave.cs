using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wave
{
    public int WaveNumber;
    public List<Airport> ActiveAirports;

    private float AirportNumber;
    private float FlightNumber;
    private float FlightInterval;



    public Wave(GameManger manger)
    {
        WaveNumber = 0;
        ActiveAirports = new List<Airport>();
        AirportNumber = manger.Settings.StartingAirportNumber;
        FlightNumber = manger.Settings.StartingFlightPerAirport;
        FlightInterval = manger.Settings.FlightTimeInterval;
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
        FlightInterval -= GameManger.Instance.Settings.FlightTimeIntervalAcceleration;

        ActiveAirports = GameManger.Instance.AirPorts.OrderBy(x => x.AppearanceOrder).Take(Mathf.FloorToInt(AirportNumber)).ToList();
        ScheuleFlights();
    }

    private void ScheuleFlights()
    {
        foreach (Airport item in ActiveAirports)
        {
            item.ScheduleFlights(Mathf.FloorToInt(FlightNumber), FlightInterval);
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
