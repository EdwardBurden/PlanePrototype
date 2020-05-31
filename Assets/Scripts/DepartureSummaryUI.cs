using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepartureSummaryUI : MonoBehaviour
{
    [SerializeField]
    private Text TimeText = null;
    [SerializeField]
    private Text DepartureText = null;
    [SerializeField]
    private Text DestinationText = null;
    [SerializeField]
    private Text PassengersText = null;

    private float TimeLeft;
    public Action FlightTime;

    public void SetFlightDetails(FlightSchedule flightSchedule)
    {
        TimeLeft = flightSchedule.TimeToDeparture;
        DepartureText.text = flightSchedule.Departure.name;
        DestinationText.text = flightSchedule.Destination.name;
        PassengersText.text = flightSchedule.Passengers.ToString();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        TimeText.text = TimeLeft.ToString("n0");
        if (TimeLeft < 0) {
            FlightTime.Invoke();
        }
    }
}
