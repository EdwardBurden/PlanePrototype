using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
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

    public int PlaneAmount;
    public float TimeInterval;
    public float TimeIntervalAcceleration;

    private void Start()
    {
        UI = Instantiate(UIPrefab, Canvas.transform);
        UI.Init(this);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int i = 0;
        while (i < PlaneAmount)
        {
            SpawnPlane();
            yield return new WaitForSeconds(TimeInterval);
            i++;
            if (TimeInterval - TimeIntervalAcceleration > 0.1)
                TimeInterval -= TimeIntervalAcceleration;
        }
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
