using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airport : MonoBehaviour
{
    public Transform DynamicSpawn;
    public Canvas Canvas;
    public AirportUI UIPrefab;
    private AirportUI UI;
    int PoolIndex;
    int PoolSize = 10;
    GameObject[] DeadPlanesPool;

    //plane generateion
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
            rigidbody.velocity = plane.GetRigidBodyVelocity();

        }
        Destroy(plane.gameObject);
        float radius = 10000;
        float power = 100000;
        Vector3 explosionPos = DeadPlanesPool[PoolIndex].transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.GetComponent<Plane>() == null)
                rb.AddExplosionForce(power, explosionPos, radius, -100.0F);
        }

        PoolIndex++;
    }


    public void SpawnPlane()
    {
        if (PlanePrefab)
        {
            Vector3 direction = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
            Vector3 target = direction + transform.position;
            Quaternion q = Quaternion.LookRotation(target, Vector3.up);

            Plane plane = Instantiate(PlanePrefab, transform.position, q, DynamicSpawn.transform);
            int passengers = Random.Range(MinPassengers, MaxPassengers);
            int fuel = Random.Range(MinFuel, MaxFuel);
            plane.Init(fuel, passengers, direction.normalized);
            plane.Death += OnDeath;
        }
    }
}
