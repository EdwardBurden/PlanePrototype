using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameManagerSettings Settings;
    public List<Airport> AirPorts;
    public PlanePool PlanePool;

    public static GameManger Instance;

    public Wave Wave;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Wave = new Wave(this);
        CameraControl.Instance.MoveCamera();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Wave.NextWave();
        }
    }


}
