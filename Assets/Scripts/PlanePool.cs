using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePool : MonoBehaviour
{
    int PoolIndex;
    int PoolSize = 10;
    GameObject[] DeadPlanesPool;

    private void Start()
    {
        DeadPlanesPool = new GameObject[PoolSize];
    }


    public void OnDeath(Plane plane)
    {
        if (PoolIndex >= PoolSize)
            PoolIndex = 0;
        if (DeadPlanesPool[PoolIndex] != null)
            Destroy(DeadPlanesPool[PoolIndex]);

        DeadPlanesPool[PoolIndex] = Instantiate(plane.DeadPlanePrefab, plane.transform.position, plane.transform.rotation, null);
        foreach (Rigidbody rigidbody in DeadPlanesPool[PoolIndex].GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.velocity = plane.GetComponent<Rigidbody>().velocity;

        }
        plane.DestroyUI();
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
}
