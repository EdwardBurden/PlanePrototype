using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DepartureListUi : MonoBehaviour
{
    public DepartureSummaryUI Summary;
    public List<DepartureSummaryUI> Summaries;

    public void UpdateFlightList(List<FlightSchedule> flightSchedules)
    {
        foreach (DepartureSummaryUI item in Summaries)
        {
            if (item)
                Destroy(item.gameObject);
        }
        Summaries = new List<DepartureSummaryUI>();
        flightSchedules = flightSchedules.OrderBy(x => x.TimeToDeparture).ToList();
        foreach (FlightSchedule flightSchedule in flightSchedules)
        {
            DepartureSummaryUI ui = Instantiate(Summary, this.transform);
            ui.SetFlightDetails(flightSchedule);
            ui.FlightTime += () => { Summaries.Remove(ui); Destroy(ui.gameObject); };
            Summaries.Add(ui);
        }

    }
}
