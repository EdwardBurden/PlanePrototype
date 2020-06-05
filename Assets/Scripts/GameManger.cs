using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public GameManagerSettings Settings;
    public List<Airport> AirPorts;
    public PlanePool PlanePool;
    public DepartureListUi FlightList;
    public EndMenu EndMenu;

    public Text WaveText;
    public static GameManger Instance;
    public Wave Wave;
    float timeleft;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Wave = new Wave(this);
        timeleft = Settings.WavePauseTime;
        StartCoroutine(NewWaveUpdate());
    }

    public void End()
    {
        Time.timeScale = 0;
        WaveText.text = "Dead";
        Wave.ActiveAirports.ForEach(x => x.enabled = false);
        EndMenu.Open();
    }

    private IEnumerator NewWaveUpdate()
    {
        while (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            WaveText.text = "New Wave Starting in: " + timeleft.ToString("n0");
            yield return null;
        }
        Wave.ScheuleFlights();
        WaveText.text = "Wave " + (Wave.WaveNumber + 1) + " Started";
        CameraControl.Instance.MoveCamera();
        FlightList.UpdateFlightList(Wave.AllFlights);
    }

    public void FlightsEmpty()
    {
        if (Wave.ActiveAirports.All(x => !x.FlightsActive))
        {
            Wave.NextWave();
            timeleft = Settings.WavePauseTime;
            StartCoroutine(NewWaveUpdate());
        }
    }
}
