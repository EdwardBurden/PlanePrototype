using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
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
    public Vector3 Directionnormalized;

    private bool Dying;
    public delegate void PlaneEvent(Plane plane);
    public event PlaneEvent Death;

    public void Init(int health, int fuel, int passengers)
    {
        Health = health;
        Fuel = fuel;
        Passengers = passengers;
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Target = direction.normalized;
        Dying = false;
        Speed = Random.Range(MinSpeed, Maxspeed);
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
            Fuel -= ((Speed * Speed) * Dampener*Time.deltaTime);
            if (Fuel > 0 && Health > 0)
            {
                Vector3 targetDirection = Target + transform.localPosition;
                Rigidbody.velocity = Target * Speed;
                this.transform.rotation = Quaternion.LookRotation(targetDirection);

            }
            else
            {
                OnDeath();
            }
        }
    }
}
