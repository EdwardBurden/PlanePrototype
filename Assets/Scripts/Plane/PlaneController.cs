using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController
{
    private enum PlaneState
    {
        Still,
        TakeOff,
        Flying,
        Landing
    }
    public float Dampener = 0.0001f;

    private PlaneState State;
    private Plane PlaneObject;

    public bool IsInteractable { get { return State == PlaneState.Flying; } }

    private Airport Origin;
    private Airport Destination;
    private Airport Target = null;

    public float Fuel;
    public int Passengers;
    public float Speed;
    public float TurningSpeed;
    public Vector3 CurrentDirection;


    public PlaneController(Plane planeObject)
    {
        PlaneObject = planeObject;
        State = PlaneState.Still;
        PlaneObject.GetComponent<Rigidbody>().isKinematic = true;
        PlaneObject.GetComponent<Collider>().enabled = false;

    }

    public void HandleUpdate()
    {
        switch (State)
        {
            case PlaneState.TakeOff:
                HandleTakeOff();
                break;
            case PlaneState.Flying:
                HandleFlying();
                break;
            case PlaneState.Landing:
                HandleLanding();
                break;
            default:
                break;
        }

    }

    private void HandleTakeOff()
    {
        PlaneObject.transform.position += ((Origin.Exit.position - PlaneObject.transform.position).normalized * Speed * Time.deltaTime);
        PlaneObject.transform.rotation = (Quaternion.LookRotation((Origin.Exit.position - PlaneObject.transform.position)).normalized);
        if (Vector3.Distance(PlaneObject.transform.position, Origin.Exit.position) < 10)
        {
            Fly();
        }
    }

    private void HandleFlying()
    {
        float decrease = ((Speed * Speed) * Dampener * Time.deltaTime);
        Fuel -= decrease;
        if (Fuel > 0)
        {
            PlaneObject.GetComponent<Rigidbody>().rotation = Quaternion.Slerp(PlaneObject.GetComponent<Rigidbody>().rotation, Quaternion.LookRotation(CurrentDirection), Time.deltaTime);
            PlaneObject.GetComponent<Rigidbody>().velocity = PlaneObject.transform.forward * Speed;
        }
        else
        {
            PlaneObject.Die();
        }
    }

    private void HandleLanding()
    {
        PlaneObject.transform.position += ((Target.SpawnPoint.position - PlaneObject.transform.position).normalized * Speed * Time.deltaTime);
        PlaneObject.transform.rotation = (Quaternion.LookRotation((Target.SpawnPoint.position - PlaneObject.transform.position)).normalized);
        if (Vector3.Distance(PlaneObject.transform.position, Target.SpawnPoint.position) < 1)
        {
            Target.OnLand(PlaneObject);
        }
    }

    public void TakeOff(int fuel, int passenegers, Vector3 direction, Airport from, Airport to)
    {
        State = PlaneState.TakeOff;
        Fuel = fuel;
        Passengers = passenegers;
        CurrentDirection = direction.normalized;
        Speed = UnityEngine.Random.Range(PlaneObject.MinSpeed, PlaneObject.Maxspeed);
        Origin = from;
        Destination = to;
        TurningSpeed = 1.0f;
    }

    public void ChangeDirection(Vector3 hitPoint)
    {
        if (State == PlaneState.Flying)
        {
            CurrentDirection = (new Vector3(hitPoint.x, PlaneObject.transform.position.y, hitPoint.z) - PlaneObject.transform.position);
            CurrentDirection = new Vector3(CurrentDirection.x, 0, CurrentDirection.z).normalized;
        }
    }

    public void Fly()
    {
        State = PlaneState.Flying;
        PlaneObject.GetComponent<Rigidbody>().isKinematic = false;
        PlaneObject.GetComponent<Collider>().enabled = true;
    }

    public void Land(Airport airport)
    {
        Target = airport;
        State = PlaneState.Landing;
        PlaneObject.GetComponent<Rigidbody>().isKinematic = true;
        PlaneObject.GetComponent<Collider>().enabled = false;

    }

}
