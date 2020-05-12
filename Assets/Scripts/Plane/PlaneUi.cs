using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneUi : MonoBehaviour
{
    public Text Passengers;
    internal void Init(Plane plane)
    {
        Passengers.GetComponent<FolloObjectUiComponent>().Init(plane.transform);
        Passengers.text = "P " + plane.GetPassengers();
    }
}
