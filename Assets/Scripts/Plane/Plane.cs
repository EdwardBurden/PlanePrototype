using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameObject DeadPlanePrefab;
    private PlaneController PlaneController;

    public PlaneUi UiPrefab;
    public Transform PlaneForward;
    private PlaneUi Ui;

    public float MinSpeed;
    public float Maxspeed;

    public delegate void PlaneEvent(Plane plane);
    public event PlaneEvent Death;

    public ParticleSystem Selection;
    private bool Selected;

    private Color DebugLineColor;

    public virtual void Awake()
    {
        Selection.Stop(); DebugLineColor = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
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

    internal float GetPassengers()
    {
        return PlaneController.Passengers;
    }

    internal float GetSpeed()
    {
        return PlaneController.Speed;
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

    internal float GetFuel()
    {
        return PlaneController.Fuel;
    }

    public void Init(int fuel, int passengers, Vector3 direction, Airport from, Airport to, Canvas canvas)
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
        PlaneForward.rotation = Quaternion.LookRotation(PlaneController.CurrentDirection);
       // PlaneForward.forward = PlaneController.CurrentDirection;
       //   Debug.DrawLine(this.transform.position, this.transform.position + (PlaneController.CurrentDirection * 200), DebugLineColor, 5);
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
