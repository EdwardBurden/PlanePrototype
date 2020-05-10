using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : InteractiveObject
{
    public float Dampener = 0.000001f;
    public int Health;
    public float Fuel;
    public int Passengers;

    public Rigidbody Rigidbody;
    public float MinSpeed;
    public float Maxspeed;
    public float Speed;

    public Vector3 Target;

    private bool Dying;
    public delegate void PlaneEvent(Plane plane);
    public event PlaneEvent Death;

    public void Init(int health, int fuel, int passengers, Vector3 direction)
    {
        Health = health;
        Fuel = fuel;
        Passengers = passengers;
        Target = direction;
        Dying = false;
        Speed = UnityEngine.Random.Range(MinSpeed, Maxspeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        OnDeath();
        //Health -= 100;
    }


    private void OnDeath()
    {
        Death.Invoke(this);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) { OnDeath(); }


        if (!Dying)
        {
            Fuel -= ((Speed * Speed) * Dampener * Time.deltaTime);
            if (Fuel > 0 && Health > 0)
            {
                Rigidbody.velocity = Target * Speed;
                Rigidbody.rotation = (Quaternion.LookRotation(Target.normalized));
            }
            else
            {
                OnDeath();
            }
        }
    }

    internal void SetTarget(Vector3 hitPoint)
    {
        Target = (new Vector3(hitPoint.x, transform.position.y, hitPoint.z) - transform.position).normalized;
    }
}
