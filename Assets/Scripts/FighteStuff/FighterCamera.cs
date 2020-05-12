using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCamera : MonoBehaviour
{
    public Camera Cam;
    public GameObject CameraPosiion;
    public GameObject FighterModel;
    public Fighter Fighter;
    public float LookLength;
    public float SpeedModifier;

    public void Update()
    {
        if (!Fighter.RotationLock)
        {
            Vector3 speeddistnace = SpeedModifier * (FighterModel.transform.forward * 1 / ((Fighter.ForwardSpeed / Fighter.MaxSpeed)));
            this.transform.position = Vector3.Lerp(this.transform.position, CameraPosiion.transform.position - speeddistnace, Time.deltaTime);
            this.transform.LookAt((FighterModel.transform.position + (FighterModel.transform.forward * LookLength)));
        }
    }
}
