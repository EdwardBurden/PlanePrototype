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

    public Text PassengersText;
    public Text TimeTillNextText;
    public Text FlightsLeftText;

    public int Passangers;
    public float Time;
    public int Flights;

    public void Init(Airport airport)
    {
        AirportController = airport;
        SpawnPoint.GetComponent<FolloObjectUiComponent>().Init(airport.SpawnPoint);
        Entrance.GetComponent<FolloObjectUiComponent>().Init(airport.Entrance);
        Exit.GetComponent<FolloObjectUiComponent>().Init(airport.Exit);
        //UpdatePassengers(airport.PassengersPool);
       // UpdateFlights(airport.FlightsLeft());
    }


    public void UpdateUI()
    {
        PassengersText.text = "P: " + Passangers.ToString();
        TimeTillNextText.text = Time.ToString();
        FlightsLeftText.text = Flights.ToString();
    }
  
}
