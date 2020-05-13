using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneUi : MonoBehaviour
{
    public Text Passengers;
    public Text Speed;
    public Text Fuel;

    private Plane Data;
    internal void Init(Plane plane)
    {
        Data = plane;
        GetComponent<FolloObjectUiComponent>().Init(plane.transform);
        Passengers.text = "P " + plane.GetPassengers();
        Speed.text = "S " + plane.GetSpeed();
        Fuel.text = "F " + plane.GetFuel();
    }

    private void Update()
    {
        if (Data)
        {

            Fuel.text = "F " + Data.GetFuel();
        }
    }
}
