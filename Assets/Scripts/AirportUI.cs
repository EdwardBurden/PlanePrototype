using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirportUI : MonoBehaviour
{
    private Airport AirportController;

    public void Init(Airport airport)
    {
        AirportController = airport;
        GetComponent<FolloObjectUiComponent>().Init(airport.transform);
    }
}
