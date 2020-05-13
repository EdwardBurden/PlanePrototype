using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameManagerSettings Settings;
    public List<Airport> AirPorts;
    public PlanePool PlanePool;

    public static GameManger Instance;

    private void Awake()
    {
        Instance = this;
    }
}
