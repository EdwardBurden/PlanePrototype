using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameManagerSettings Settings;
    public List<Airport> AirPorts;
    public PlanePool PlanePool;

    public static GameManger Instance;

    private Wave Wave;
    private void Awake()
    {
        Instance = this;
        Wave = new Wave(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Wave.NextWave();
        }
    }


}
