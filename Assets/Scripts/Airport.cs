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
    public int MaxFuel;
    public int MinFuel;
    public int MaxPassengers;
    public int MinPassengers;
    public int PassengersPool;

    public delegate void WaveEvent();
    public event WaveEvent AirportEmptied;

    public bool FlightsActive;

    private float TimeToNextSpawn;

    private void Start()
    {
        UI = Instantiate(UIPrefab, Canvas.transform);
        UI.Init(this);
        FlightsActive = false;
    }

    private void Update()
    {
        TimeToNextSpawn -= Time.deltaTime;
        UI.UpdateTime(TimeToNextSpawn);
    }

    private IEnumerator Spawn(int amount, float mintime, float maxtime)
    {
        FlightsActive = true;
        int i = 0;
        while (i < amount)
        {
            TimeToNextSpawn = Random.Range(mintime, maxtime);
            Debug.Log(TimeToNextSpawn);
            yield return new WaitForSeconds(TimeToNextSpawn);
            SpawnPlane();
            i++;
        }
        FlightsActive = false;
        AirportEmptied.Invoke();
    }

    internal void ScheduleFlights(int flightNumber, float min, float max)
    {
        StartCoroutine(Spawn(flightNumber, min, max));

    }

    public void OnLand(Plane plane)
    {
        plane.DestroyUI();
        PassengersPool += (int)plane.GetPassengers();
        UI.UpdatePassengers(PassengersPool);
        Destroy(plane.gameObject);
    }

    public void SpawnPlane()
    {

        Vector3 direction = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
        Plane plane = Instantiate(GameManger.Instance.Settings.PlanePrefab, transform.position, Quaternion.identity, DynamicSpawn.transform);
        int passengers = Random.Range(MinPassengers, MaxPassengers); PassengersPool -= passengers;
        int fuel = Random.Range(MinFuel, MaxFuel);
        plane.Init(fuel, passengers, direction, this, null, Canvas);
        plane.Death += GameManger.Instance.PlanePool.OnDeath;
        UI.UpdatePassengers(PassengersPool);

    }
}
