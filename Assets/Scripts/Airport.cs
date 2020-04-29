using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    int PoolIndex;
    int PoolSize = 10;
    GameObject[] DeadPlanesPool;

    //plane generateion
    public int MaxHealth;
    public int MinHealth;
    public int MaxFuel;
    public int MinFuel;
    public int MaxPassengers;
    public int MinPassengers;


    public int PlaneAmount;
    public float TimeInterval;
    public float TimeIntervalAcceleration;
    private void Start()
    {
        DeadPlanesPool = new GameObject[PoolSize];
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
    public Plane PlanePrefab;
    public GameObject PlaneDead;




    public void OnDeath(Plane plane)
    {
        if (PoolIndex >= PoolSize)
            PoolIndex = 0;
        if (DeadPlanesPool[PoolIndex] != null)
            Destroy(DeadPlanesPool[PoolIndex]);

        DeadPlanesPool[PoolIndex] = Instantiate(PlaneDead, plane.transform.position, plane.transform.rotation, null);
        foreach (Rigidbody rigidbody in DeadPlanesPool[PoolIndex].GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.velocity = plane.Rigidbody.velocity;

        }
        Destroy(plane.gameObject);
        PoolIndex++;
    }


    public void SpawnPlane()
    {
        if (PlanePrefab)
        {
            Plane plane = Instantiate(PlanePrefab, this.transform);
            int passengers = Random.Range(MinPassengers, MaxPassengers);
            int health = Random.Range(MinHealth, MaxHealth);
            int fuel = Random.Range(MinFuel, MaxFuel);
            plane.Init(health, fuel, passengers);
            plane.Death += OnDeath;
        }
    }
}
