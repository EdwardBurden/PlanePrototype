using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameManagerSettings Settings;
    public List<Airport> AirPorts;
    public PlanePool PlanePool;
    public DepartureListUi FlightList;

    public static GameManger Instance;

    public Wave Wave;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Wave = new Wave(this);
        CameraControl.Instance.MoveCamera();
        FlightList.UpdateFlightList(Wave.AllFlights);
    }

    public void AirportEmpty()
    {
        if (Wave.ActiveAirports.All(x => !x.FlightsActive))
        {
            Wave.NextWave();
            CameraControl.Instance.MoveCamera();
            FlightList.UpdateFlightList(Wave.AllFlights);
        }
    }
}
