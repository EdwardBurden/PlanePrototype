using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirportUI : MonoBehaviour
{
    private Airport AirportController;

    public Image SpawnPoint;

    public Image Entrance;

    public Image Exit;

    public Text Passengers;

    public void Init(Airport airport)
    {
        AirportController = airport;
        SpawnPoint.GetComponent<FolloObjectUiComponent>().Init(airport.SpawnPoint);
        Entrance.GetComponent<FolloObjectUiComponent>().Init(airport.Entrance);
        Exit.GetComponent<FolloObjectUiComponent>().Init(airport.Exit);
        UpdatePassengers(airport.PassengersPool);
    }

    internal void UpdatePassengers(int passengersPool)
    {
        Passengers.text = "P: " + passengersPool;
    }
}
