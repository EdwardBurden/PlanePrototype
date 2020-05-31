using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Airport : MonoBehaviour
{
    public int AppearanceOrder;

    public Transform Entrance;
    public Transform SpawnPoint;
    public Transform Exit;

    public Transform DynamicSpawn;
    public Canvas Canvas;
    public AirportUI UIPrefab;
    AirportUI UI;

    //plane generateion
    public int PassengersPool;

    public Action AirportEmptied;

    public bool FlightsActive;

    private float TimeToNextSpawn;
    private int SpawnIndex;
    private List<FlightSchedule> Flights;

    private void Start()
    {
        UI = Instantiate(UIPrefab, Canvas.transform);
        UI.Init(this);
        FlightsActive = true;
        SpawnIndex = 0;
    }

    public void SetFlights(List<FlightSchedule> flightSchedules)
    {
        SpawnIndex = 0;
        Flights = flightSchedules;
        FlightsActive = true;
        TimeToNextSpawn = 0;
    }

    private void Update()
    {
        if (Flights != null && SpawnIndex < Flights.Count)
        {
            if (TimeToNextSpawn > Flights[SpawnIndex].TimeToDeparture)
            {
                SpawnPlane(Flights[SpawnIndex]);
                SpawnIndex++;
                if (SpawnIndex == Flights.Count)
                {
                    FlightsActive = false;
                    AirportEmptied.Invoke();
                }
            }
        }
        TimeToNextSpawn += Time.deltaTime;
    }

    public void OnLand(Plane plane)
    {
        plane.DestroyUI();
        PassengersPool += (int)plane.GetPassengers();
        Destroy(plane.gameObject);
    }

    public void SpawnPlane(FlightSchedule schedule)
    {
        Plane plane = Instantiate(GameManger.Instance.Settings.PlanePrefab, transform.position, Quaternion.identity, DynamicSpawn.transform);
        Vector3 random = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
        Vector3 direction = ((schedule.Destination.transform.position + random) - schedule.Departure.transform.position).normalized;
        PassengersPool -= schedule.Passengers;
        plane.Init(schedule.Fuel, schedule.Passengers, direction, this, null, Canvas, schedule);
        plane.Death += GameManger.Instance.PlanePool.OnDeath;
    }
}
