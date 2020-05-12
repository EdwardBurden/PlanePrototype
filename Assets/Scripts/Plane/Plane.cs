using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Airport DepartedAirport;
    public Airport TargetAirport;

    public float Dampener = 0.000001f;
    public float Fuel;
    public int Passengers;
    public Rigidbody Rigidbody;
    public float MinSpeed;
    public float Maxspeed;
    public float Speed;
    public Vector3 Target;
    public bool Dying;
    public delegate void PlaneEvent(Plane plane);
    public event PlaneEvent Death;
    public PlaneState State;
    public ParticleSystem Selection;


    public virtual void Awake()
    {
        Selection.Stop();
    }

    public void OnHover()
    {
        State.OnHover();
    }
    public void OffHover()
    {
        State.OffHover();
    }

    public void OnClick()
    {
        State.OnClick();
    }
    public void OffClick()
    {
        State.OffClick();
    }

    public void ChangeState(Type type)
    {
        if (State != null)
            State.DeActivate();
        State = (PlaneState)Activator.CreateInstance(type);
        State.Activate(this);
    }

    public void Init(int fuel, int passengers, Vector3 direction, Airport from, Airport to)
    {
        ChangeState(typeof(TakeOffState));
        Fuel = fuel;
        Passengers = passengers;
        Target = direction;
        Dying = false;
        Speed = UnityEngine.Random.Range(MinSpeed, Maxspeed);
        DepartedAirport = from;
        TargetAirport = to;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Airport"))
        {
            ChangeState(typeof(LandingState));
        }
        else
        {
            Die();
        }
    }

    private void Update()
    {
        State.HandleUpdateBehaviour();
    }

    public void Die()
    {
        Death.Invoke(this);
    }

    public void Rest()
    {
        Debug.Log("Success");
        Destroy(this.gameObject);

    }

    internal void SetTarget(Vector3 hitPoint)
    {
        Target = (new Vector3(hitPoint.x, transform.position.y, hitPoint.z) - transform.position).normalized;
    }

    internal Vector3 GetRigidBodyVelocity()
    {
        return Rigidbody.velocity;
    }
}
