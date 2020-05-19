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

    private float Timer;

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

        Timer += Time.deltaTime;
        PlaneObject.transform.position = Vector3.Lerp(Origin.SpawnPoint.position, Origin.Exit.position, Timer);
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
        Timer += Time.deltaTime;
        PlaneObject.transform.position = Vector3.Lerp(Target.Entrance.position, Target.SpawnPoint.position, Timer);
        PlaneObject.transform.rotation = (Quaternion.LookRotation((PlaneObject.transform.position - Target.Entrance.position)).normalized);
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
        Timer = 0;
        CurrentDirection = (new Vector3(hitPoint.x, PlaneObject.transform.position.y, hitPoint.z) - PlaneObject.transform.position).normalized;
    }

    public void Fly()
    {
        Timer = 0;
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
