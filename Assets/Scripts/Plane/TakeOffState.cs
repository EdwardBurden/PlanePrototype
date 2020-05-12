using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOffState : PlaneState
{
    private Plane Plane;
    private float Timme = 0;
    public override void Activate(Plane plane)
    {
        Plane = plane;
        plane.Rigidbody.isKinematic = true;
        plane.GetComponent<Collider>().enabled = false;
    }

    public override void DeActivate()
    {
        //  throw new System.NotImplementedException();
    }

    public override void HandleUpdateBehaviour()
    {
        Timme += Time.deltaTime;
        Plane.transform.position = Vector3.Lerp(Plane.DepartedAirport.SpawnPoint.position, Plane.DepartedAirport.Exit.position, Timme);
        Plane.transform.rotation = (Quaternion.LookRotation((Plane.DepartedAirport.Exit.position - Plane.transform.position)).normalized);
        if (Vector3.Distance(Plane.transform.position, Plane.DepartedAirport.Exit.position) < 1)
        {
            Plane.ChangeState(typeof(FlyingState));
        }
    }

    public override void OffClick()
    {
        // throw new System.NotImplementedException();
    }

    public override void OffHover()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnClick()
    {
        // throw new System.NotImplementedException();
    }

    public override void OnHover()
    {
        //throw new System.NotImplementedException();
    }
}
