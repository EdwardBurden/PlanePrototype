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
    public Text AirportName;
    public Text PassengerAmount;

    public void Init(Airport airport)
    {
        AirportName.text = airport.name;
        AirportController = airport;
        SpawnPoint.GetComponent<FolloObjectUiComponent>().Init(airport.SpawnPoint);
        Entrance.GetComponent<FolloObjectUiComponent>().Init(airport.Entrance);
        Exit.GetComponent<FolloObjectUiComponent>().Init(airport.Exit);
    }

    internal void UpdatePassengers(int passengersPool)
    {
        PassengerAmount.text = passengersPool.ToString();
    }
}
