using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private PlaneController PlaneController;

    public PlaneUi UiPrefab;

    private PlaneUi Ui;

    public float MinSpeed;
    public float Maxspeed;

    public delegate void PlaneEvent(Plane plane);
    public event PlaneEvent Death;

    public ParticleSystem Selection;
    private bool Selected;

    public virtual void Awake()
    {
        Selection.Stop();
    }

    public void OnHover()
    {
        if (PlaneController.IsInteractable)
        {
            if (!Selected)
            {
                ParticleSystem.MainModule main = Selection.main;
                main.startColor = new ParticleSystem.MinMaxGradient(Color.blue);
                Selection.Play();
            }
        }

    }

    public void OffHover()
    {
        if (PlaneController.IsInteractable)
        {
            if (!Selected)
            {
                Selection.Stop();
            }
        }
    }

    public void OnClick()
    {
        if (PlaneController.IsInteractable)
        {
            Selected = true;
            ParticleSystem.MainModule main = Selection.main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.green); Selection.Play();
        }

    }
    public void OffClick()
    {
        if (PlaneController.IsInteractable)
        {
            Selected = false;
            Selection.Stop();
        }
    }

    internal int GetPassengers()
    {
        return PlaneController.Passengers;
    }

    public void Init(int fuel, int passengers, Vector3 direction, Airport from, Airport to , Canvas canvas)
    {
        Selection.Stop();
        PlaneController = new PlaneController(this);
        PlaneController.TakeOff(fuel, passengers, direction, from, to);
        Ui = Instantiate(UiPrefab, canvas.transform);
        Ui.Init(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Airport"))
        {
            PlaneController.Land(collision.gameObject.GetComponentInParent<Airport>());
        }
        else
        {
            Die();
        }
    }

    internal void DestroyUI()
    {
        Destroy(Ui.gameObject);
    }

    private void Update()
    {
        PlaneController.HandleUpdate();
    }

    public void Die()
    {
        Death.Invoke(this);
    }

    internal void SetTarget(Vector3 hitPoint)
    {
        PlaneController.ChangeDirection(hitPoint);
    }
}
